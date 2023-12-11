using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FrontAceGerenciador.Pages.Shared
{
    public class ClientesModel : PageModel
    {
        public List<Cliente> ClientesList { get; set; } = new();

        public void OnGet([FromRoute]int skip=0, [FromRoute]int take=10)
        {
            List<Cliente> clientes = new();

            for (int i = 0; i < 1000; i++)
            {
                clientes.Add(new Cliente(i,
                    $"NomeCliente {i}",
                    $"EnderecoCliente {i}",
                    $"EmailCliente {i}",
                    $"TelefoneCliente {i}",
                    $"TipoCliente {i}")
                );
            }

            ClientesList = clientes
                .Skip(skip)
                .Take(take)  // Corrigido aqui
                .ToList();
        }
    }

    public record Cliente(
        int? ClienteId,
        string NomeCliente,
        string EnderecoCliente,
        string EmailCliente,
        string TelefoneCliente,
        string TipoPet
    );
}
