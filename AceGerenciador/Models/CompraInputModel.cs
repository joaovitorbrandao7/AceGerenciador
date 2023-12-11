using Microsoft.AspNetCore.Mvc;

namespace AceGerenciador.Models
{
    public class CompraInputModel
    {
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }
        public List<int> ProdutosIds { get; set; }
    }


}