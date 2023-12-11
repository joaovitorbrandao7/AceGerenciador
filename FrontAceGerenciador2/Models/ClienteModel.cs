using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontAceGerenciador2.Models
{
    public class ClienteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdCliente { get; set; }
        [Required(ErrorMessage="É obrigatório")]
        public string NomeCliente { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string EmailCliente { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string EnderecoCliente { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string TelefoneCliente { get; set; }
        [Required(ErrorMessage = "É obrigatório")]
        public string TipoPet { get; set; }

    }
}
