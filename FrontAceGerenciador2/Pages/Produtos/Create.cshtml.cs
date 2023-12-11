using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Newtonsoft.Json;

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var httpProduto = new HttpClient();
                var url = "http://localhost:5151/api/Produtos";
                var serializedProduto = JsonConvert.SerializeObject(ProdutoModel);
                var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");

                var response = await httpProduto.PostAsync(url, content);

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
