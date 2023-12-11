using AceGerenciador.Data;
using AceGerenciador.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AceGerenciador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComprasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var compras = _context.Compras.ToList();
            return Ok(compras);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var compra = _context.Compras.FirstOrDefault(c => c.IdCompra == id);

            if (compra == null)
            {
                return NotFound();
            }

            return Ok(compra);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Compra compra)
        {
            try
            {
                // Aqui você pode adicionar lógica para validar e salvar a compra no banco de dados
                _context.Compras.Add(compra);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = compra.IdCompra }, compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
