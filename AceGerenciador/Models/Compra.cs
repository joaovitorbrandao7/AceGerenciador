using System;
using System.Collections.Generic;

namespace AceGerenciador.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public ICollection<Produto> Produtos { get; private set; } = new List<Produto>();
        public decimal TotalCompra { get; set; }
        public Funcionario Funcionario { get; set; }

        public Compra() { }

        public Compra(int idCompra, DateTime dataCompra, decimal totalCompra, Funcionario funcionario)
        {
            IdCompra = idCompra;
            DataCompra = dataCompra;
            TotalCompra = totalCompra;
            Funcionario = funcionario;
        }

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }
    }
}