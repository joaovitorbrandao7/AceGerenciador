using AceGerenciador.Data;
using AceGerenciador.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AceGerenciador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioDiarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatorioDiarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(DateTime dataRelatorio)
        {
            try
            {
                var vendasDoDia = _context.Venda
                    .Where(v => v.DataVenda.Date == dataRelatorio.Date)
                    .ToList();

                var totalVendasDia = vendasDoDia.Sum(v => v.TotalVenda);

                var relatorioDiario = new RelatorioDiario
                {
                    DataRelatorio = dataRelatorio,
                    VendasDia = vendasDoDia,
                    TotalVendasDia = totalVendasDia
                };

                return Ok(relatorioDiario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
