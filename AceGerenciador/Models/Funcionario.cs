using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AceGerenciador.Models
{
    public class Funcionario
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

        public Funcionario() { }

        public Funcionario(string nomeFuncionario, string cargoFuncionario, DateTime dataAdmissao)
        {
            NomeFuncionario = nomeFuncionario;
            CargoFuncionario = cargoFuncionario;
            DataAdmissao = dataAdmissao;
        }
    }
}
