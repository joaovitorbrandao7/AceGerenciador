using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Clientes
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClienteModel ClienteModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClienteModel = await _context.Clientes.FirstOrDefaultAsync(m => m.IdCliente == id);

            if (ClienteModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clienteToUpdate = await _context.Clientes.FindAsync(id);

            if (clienteToUpdate == null)
            {
                return NotFound();
            }

            clienteToUpdate.NomeCliente = ClienteModel.NomeCliente;
            clienteToUpdate.EmailCliente = ClienteModel.EmailCliente;
            clienteToUpdate.TelefoneCliente = ClienteModel.TelefoneCliente;
            clienteToUpdate.EnderecoCliente = ClienteModel.EnderecoCliente;
            clienteToUpdate.TipoPet = ClienteModel.TipoPet;

            try
            {
                var httpClient = new HttpClient();
                var url = $"http://localhost:5151/api/Clientes/{id}"; // Substitua isso pela URL correta da sua API
                var serializedCliente = JsonConvert.SerializeObject(ClienteModel);
                var content = new StringContent(serializedCliente, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Clientes");
                }
                else
                {
                    // Lógica de tratamento de erro, se necessário
                    return Page();
                }
            }
            catch (HttpRequestException)
            {
                // Lógica de tratamento de erro, se necessário
                return Page();
            }
        }
    }
}
