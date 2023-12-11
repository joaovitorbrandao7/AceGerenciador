namespace AceGerenciador.Models
{
    // Classe base para Produto e ProdutoVendido
    public abstract class ProdutoBase
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeVendida { get; set; }
        public decimal PrecoUnitarioVenda { get; set; }

        // Adicione métodos comuns, se necessário
        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}, Preço: {Preco:C}, Categoria: {Categoria}, Descrição: {Descricao}");
        }
    }
}
