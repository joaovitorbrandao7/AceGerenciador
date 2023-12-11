namespace AceGerenciador.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeVendida { get; set; }
        public decimal PrecoUnitarioVenda { get; set; }

        // Construtor padrão sem parâmetros
        public Produto()
        {
        }

        public Produto(int produtoId, string nome, decimal preco, int quantidadeEmEstoque, string categoria, string descricao)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
            Categoria = categoria;
            Descricao = descricao;
        }
    }
}