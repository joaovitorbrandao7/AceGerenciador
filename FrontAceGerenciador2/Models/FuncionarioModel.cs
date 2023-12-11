using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FrontAceGerenciador2.Models
{
    public class FuncionarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "É obrigatório")]
        public string NomeFuncionario { get; set; }

        [Required(ErrorMessage = "É obrigatório")]
        public string CargoFuncionario { get; set; }

        [Required(ErrorMessage = "É obrigatório")]
        public DateTime DataAdmissao { get; set; }

        public FuncionarioModel() { }

        public FuncionarioModel(string nomeFuncionario, string cargoFuncionario, DateTime dataAdmissao)
        {
            NomeFuncionario = nomeFuncionario;
            CargoFuncionario = cargoFuncionario;
            DataAdmissao = dataAdmissao;
        }
    }
}
