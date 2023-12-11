using System;
using System.Collections.Generic;

namespace AceGerenciador.Models
{
    public class Venda
    {
        public int VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal TotalVenda { get; set; }

        public ICollection<ProdutoVendido> ProdutosVendidos { get; set; } = new List<ProdutoVendido>();

        public Funcionario Funcionario { get; set; }
        public int ClienteId { get; set; }

        public Clientes Clientes { get; set; }
        public int FuncionarioId { get; set; }

        public void CalcularTotalVenda()
        {
            if (ProdutosVendidos != null && ProdutosVendidos.Any())
            {
                TotalVenda = ProdutosVendidos.Sum(produtoVendido => produtoVendido.PrecoUnitario * produtoVendido.Quantidade);
            }
            else
            {
                TotalVenda = 0;
            }
        }
    }
}

