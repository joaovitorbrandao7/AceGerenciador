using AceGerenciador.Data;
using AceGerenciador.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AceGerenciador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return Created($"/api/produtos/{produto.ProdutoId}", produto);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Produto produto)
        {
            var existingProduto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (existingProduto == null)
            {
                return NotFound();
            }

            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            existingProduto.QuantidadeEmEstoque = produto.QuantidadeEmEstoque;
            existingProduto.Categoria = produto.Categoria;
            existingProduto.Descricao = produto.Descricao;

            _context.Produtos.Update(existingProduto);
            _context.SaveChanges();

            return Ok(existingProduto);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
