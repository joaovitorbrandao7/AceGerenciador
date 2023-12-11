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
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var funcionarios = _context.Funcionario.ToList();
            return Ok(funcionarios);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(x => x.IdFuncionario == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Funcionario funcionario)
        {
            _context.Funcionario.Add(funcionario);
            _context.SaveChanges();
            return Created($"/api/funcionarios/{funcionario.IdFuncionario}", funcionario);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Funcionario funcionario)
        {
            var existingFuncionario = _context.Funcionario.FirstOrDefault(x => x.IdFuncionario == id);

            if (existingFuncionario == null)
            {
                return NotFound();
            }

            existingFuncionario.NomeFuncionario = funcionario.NomeFuncionario;
            existingFuncionario.DataAdmissao = funcionario.DataAdmissao;
            existingFuncionario.CargoFuncionario = funcionario.CargoFuncionario;

            _context.Funcionario.Update(existingFuncionario);
            _context.SaveChanges();

            return Ok(existingFuncionario);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(x => x.IdFuncionario == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionario.Remove(funcionario);
            _context.SaveChanges();

            return Ok(funcionario);
        }
    }
}
