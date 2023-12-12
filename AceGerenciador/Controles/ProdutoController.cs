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
            if (produto == null || id != produto.ProdutoId)
            {
                return BadRequest(); // Bad request if the product or IDs don't match
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            var existingProduto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);

            if (existingProduto == null)
            {
                return NotFound(); // Product not found
            }

            // Update only the properties that are allowed to be updated
            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            existingProduto.QuantidadeEmEstoque = produto.QuantidadeEmEstoque;
            existingProduto.Categoria = produto.Categoria;
            existingProduto.Descricao = produto.Descricao;

            try
            {
                _context.SaveChanges();
                return Ok(existingProduto);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues if needed
                // For example, you can reload the entity and try updating again
                // or return a specific error response
                return StatusCode(409, "Concurrency conflict");
            }
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
