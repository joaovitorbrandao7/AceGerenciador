using FrontAceGerenciador.Pages.Shared;
using Microsoft.EntityFrameworkCore;

namespace FrontAceGerenciador.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();


            if (context.Clientes.Any())
            {
                return;
            }

            var clientes = new Cliente[]
            {
                new Cliente(
                    null, // ClienteId is nullable, so pass null here
                    "João",
                    "Rua Manaus",
                    "(45)988057021",
                    "joao@joao.com",
                    "Cachorro"
                ),
            };

            context.Clientes.AddRange(clientes);
            context.SaveChanges();
        }
    }
}
