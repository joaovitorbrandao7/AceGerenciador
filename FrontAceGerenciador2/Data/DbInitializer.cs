using FrontAceGerenciador2.Models;

namespace FrontAceGerenciador2.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Clientes!.Any()) // Renomeei para "Clientes"
            {
                return;
            }

            var clientes = new ClienteModel[]
            {
            new ClienteModel
            {
                NomeCliente = "João",
                EmailCliente = "joao@joao.com",
                TelefoneCliente = "(45)988057021",
                EnderecoCliente = "Rua Manaus",
                TipoPet = "Cachorro"
            },
            };

            context.AddRange(clientes);
            context.SaveChanges();
        }
    }

}
