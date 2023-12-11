using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontAceGerenciador2.Models
{
    public class ProdutoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public int QuantidadeEmEstoque { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public int QuantidadeVendida { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public decimal PrecoUnitarioVenda { get; set; }

        // Construtor padrão sem parâmetros
        public ProdutoModel()
        {
        }

        public ProdutoModel(int produtoId, string nome, decimal preco, int quantidadeEmEstoque, string categoria, string descricao)
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
