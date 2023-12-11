using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using FrontAceGerenciador2.Data;
using System.Net;

namespace FrontAceGerenciador2.Pages.Clientes
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClienteModel ClienteModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClienteModel = await _context.Clientes.FindAsync(id);

            if (ClienteModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"http://localhost:5151/api/Clientes/{id}";
                    var response = await httpClient.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var clienteToDelete = await _context.Clientes.FindAsync(id);

                        if (clienteToDelete != null)
                        {
                            _context.Remove(clienteToDelete);
                            await _context.SaveChangesAsync();
                        }

                        return RedirectToPage("Clientes");
                    }
                    else
                    {
                        // Log detalhes da resposta para diagnóstico
                        Console.WriteLine($"DELETE request failed. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                        // Handle other status codes if needed
                        ModelState.AddModelError(string.Empty, "Falha ao excluir o Funcionario. Código de status: " + response.StatusCode);
                        return Page();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Log detalhes da exceção para diagnóstico
                Console.WriteLine($"HTTP request failed with exception: {ex.Message}");

                // Handle HttpRequestException
                ModelState.AddModelError(string.Empty, "Erro ao realizar a solicitação HTTP: " + ex.Message);
                return Page();
            }
        }



    }
}
