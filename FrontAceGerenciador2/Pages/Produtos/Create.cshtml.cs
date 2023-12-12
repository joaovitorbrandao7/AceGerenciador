using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FrontAceGerenciador2.Data;

namespace FrontAceGerenciador2.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        public List<ProdutoModel> ProdutoList { get; set; } = new();

        public CreateModel() { }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:5151/";
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            ProdutoList = JsonConvert.DeserializeObject<List<ProdutoModel>>(content!);

            return Page();
        }

        [BindProperty]
        public ProdutoModel ProdutoModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var httpClient = new HttpClient();
                var url = "http://localhost:5151/api/Produtos";
                var serializedCliente = JsonConvert.SerializeObject(ProdutoModel);
                var content = new StringContent(serializedCliente, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // Você pode redirecionar para a página de listagem ou fazer algo mais
                    return RedirectToPage("Produtos");
                }
                else
                {
                    return Page();
                }
            }
            catch (HttpRequestException)
            {
                return Page();
            }
        }
    }
}
