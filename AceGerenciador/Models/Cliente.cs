namespace AceGerenciador.Models
{
    public class Clientes
    {
        public int? IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TipoPet { get; set; }

        public Clientes(int? idCliente, string nomeCliente, string enderecoCliente, string telefoneCliente, string emailCliente, string tipoPet)
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
