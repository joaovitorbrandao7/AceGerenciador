using System;
using System.Collections.Generic;

namespace AceGerenciador.Models
{
    public class RelatorioDiario
    {
        public int IdRelatorio { get; set; }
        public DateTime DataRelatorio { get; set; }
        public decimal TotalVendasDia { get; set; }
        public ICollection<Venda> VendasDia { get; set; } = new List<Venda>(); 

        public RelatorioDiario() { }

        public RelatorioDiario(int? idRelatorio, DateTime dataRelatorio, decimal totalVendasDia)
        {
            IdRelatorio = idRelatorio ?? default(int);
            DataRelatorio = dataRelatorio;
            TotalVendasDia = totalVendasDia;
        }
    }
}
