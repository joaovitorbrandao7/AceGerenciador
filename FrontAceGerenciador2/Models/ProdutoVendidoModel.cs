using AceGerenciador.Models;

namespace FrontAceGerenciador2.Models
{
    public class ProdutoVendidoModel
    {
        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

        public int VendaId { get; set; }
        public VendaModel Venda { get; set; }
        public int FuncionarioId { get; set; }
        public FuncionarioModel Funcionario { get; set; } // Adicionando referência ao Funcionario

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
