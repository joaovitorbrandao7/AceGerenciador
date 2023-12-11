namespace FrontAceGerenciador2.Models
{
    public class VendaInputModel
    {
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }
        public List<ProdutoVendidoModel> ProdutosVendidos { get; set; }
    }

    public class ProdutoVendidoInputModel
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}

