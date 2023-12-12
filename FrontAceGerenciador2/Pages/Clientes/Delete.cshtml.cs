using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FrontAceGerenciador2.Data;

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

            
            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Clientes/{id}"; 
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            ClienteModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ClienteModel>(content);

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
                     

                        return RedirectToPage("Clientes");
                    }
                    else
                    {
                        Console.WriteLine($"DELETE request failed. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                        ModelState.AddModelError(string.Empty, "Falha ao excluir o Cliente. Código de status: " + response.StatusCode);
                        return Page();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed with exception: {ex.Message}");

                ModelState.AddModelError(string.Empty, "Erro ao realizar a solicitação HTTP: " + ex.Message);
                return Page();
            }
        }
    }
}
