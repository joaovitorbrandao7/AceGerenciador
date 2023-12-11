// VendaInputModel
using System;
using System.Collections.Generic;

namespace AceGerenciador.Models
{
    public class VendaInputModel
    {
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }
        public List<ProdutoVendidoInputModel> ProdutosVendidos { get; set; }
    }

    public class ProdutoVendidoInputModel
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

}
