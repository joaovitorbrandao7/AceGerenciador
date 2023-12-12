using AceGerenciador.Data;
using AceGerenciador.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AceGerenciador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var vendas = _context.Venda
                    .Include(v => v.Funcionario)
                    .Include(v => v.ProdutosVendidos)
                    .Include(v => v.Clientes) 
                    .ToList();

                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };

                var vendasComClienteId = vendas.Select(v =>
                {
                    v.ClienteId = v.Clientes?.IdCliente ?? 0;
                    return v;
                });
                
                var json = JsonSerializer.Serialize(vendasComClienteId, jsonOptions);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Erro ao recuperar as vendas: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var venda = _context.Venda
                    .Include(v => v.ProdutosVendidos)
                    .FirstOrDefault(v => v.VendaId == id);

                if (venda != null)
                {
                    var jsonOptions = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        // Outras opções, se necessário
                    };

                    var json = JsonSerializer.Serialize(venda, jsonOptions);

                    return Ok(json);
                }
                else
                {
                    return NotFound(new { Mensagem = $"Venda com ID {id} não encontrada." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = $"Erro ao recuperar a venda: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] VendaInputModel vendaInputModel)
        {
            try
            {
                var novaVenda = CriarNovaVenda(vendaInputModel);

                // Salvar a venda no banco diretamente aqui
                _context.Venda.Add(novaVenda);
                _context.SaveChanges();

                // Restante do seu código...

                return Ok(new { VendaId = novaVenda.VendaId, Mensagem = "Venda criada com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return BadRequest(new { Mensagem = $"Erro ao criar a venda: {ex.Message}" });
            }
        }

        private Venda CriarNovaVenda(VendaInputModel vendaInputModel)
        {
            var funcionario = _context.Funcionario.Find(vendaInputModel.FuncionarioId);

            if (funcionario == null)
            {
                throw new Exception($"Funcionário com ID {vendaInputModel.FuncionarioId} não encontrado.");
            }

            var novaVenda = new Venda
            {
                DataVenda = DateTime.Now,
                ProdutosVendidos = new List<ProdutoVendido>(),
                ClienteId = vendaInputModel.ClienteId,
                FuncionarioId = vendaInputModel.FuncionarioId,
                Funcionario = funcionario
            };

            foreach (var produtoVendidoInput in vendaInputModel.ProdutosVendidos)
            {
                var produto = _context.Produtos.Find(produtoVendidoInput.ProdutoId);

                if (produto != null)
                {
                    if (produto.QuantidadeEmEstoque >= produtoVendidoInput.Quantidade)
                    {
                        var produtoVendido = new ProdutoVendido
                        {
                            Produto = produto,
                            Quantidade = produtoVendidoInput.Quantidade,
                            PrecoUnitario = produto.Preco,
                            FuncionarioId = vendaInputModel.FuncionarioId
                        };

                        novaVenda.ProdutosVendidos.Add(produtoVendido);

                        // Atualizar a quantidade em estoque
                        produto.QuantidadeEmEstoque -= produtoVendidoInput.Quantidade;
                        produto.QuantidadeVendida += produtoVendidoInput.Quantidade;
                    }
                    else
                    {
                        // Lide com a situação em que a quantidade em estoque é insuficiente
                        throw new Exception($"Quantidade em estoque insuficiente para o produto com ID {produtoVendidoInput.ProdutoId}.");
                    }
                }
                else
                {
                    // Lide com a situação em que o produto não é encontrado
                    throw new Exception($"Produto com ID {produtoVendidoInput.ProdutoId} não encontrado.");
                }
            }

            novaVenda.CalcularTotalVenda();

            return novaVenda;
        }


    }
}
