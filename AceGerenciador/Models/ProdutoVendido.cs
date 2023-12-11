namespace AceGerenciador.Models
{
    public class ProdutoVendido
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; } // Adicionando referência ao Funcionario

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
