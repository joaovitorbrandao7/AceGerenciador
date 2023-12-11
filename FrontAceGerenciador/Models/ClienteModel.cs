using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FrontAceGerenciador.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdCliente { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string NomeCliente { get; set; }
        [Required(ErrorMessage = "Endereço é obrigatório")]
        public string EnderecoCliente { get; set; }
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string TelefoneCliente { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório")]
        public string EmailCliente { get; set; }
        [Required(ErrorMessage = "Tipo do Pet é obrigatório")]
        public string TipoPet { get; set; }

        public Cliente(int? idCliente, string nomeCliente, string enderecoCliente, string telefoneCliente, string emailCliente, string tipoPet)
        {
            IdCliente = idCliente;
            NomeCliente = nomeCliente;
            EnderecoCliente = enderecoCliente;
            EmailCliente = emailCliente;
            TelefoneCliente = telefoneCliente;
            TipoPet = tipoPet;
        }
    }

}


