using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontAceGerenciador2.Pages.Produtos
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public ProdutoModel ProdutoModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Produtos/{id}";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            ProdutoModel = JsonConvert.DeserializeObject<ProdutoModel>(content);

            if (ProdutoModel == null)
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
                    var url = $"http://localhost:5151/api/Produtos/{id}";
                    var response = await httpClient.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage("Produtos");
                    }
                    else
                    {

                        return Page();
                    }
                }
            }
            catch (HttpRequestException)
            {

                return Page();
            }
        }
    }
}
