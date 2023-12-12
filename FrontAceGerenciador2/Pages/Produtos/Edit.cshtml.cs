using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Produtos
{
    public class EditModel : PageModel
    {
        public List<ProdutoModel> ProdutoList { get; set; } = new();


        [BindProperty]
        public ProdutoModel ProdutoModel { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Produtos/{id}";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            var produtoToUpdate = JsonConvert.DeserializeObject<ProdutoModel>(content);

            produtoToUpdate.Nome = ProdutoModel.Nome;
            produtoToUpdate.Descricao = ProdutoModel.Descricao;
            produtoToUpdate.QuantidadeEmEstoque = ProdutoModel.QuantidadeEmEstoque;
            produtoToUpdate.QuantidadeVendida = ProdutoModel.QuantidadeVendida;
            produtoToUpdate.PrecoUnitarioVenda = ProdutoModel.PrecoUnitarioVenda;
            produtoToUpdate.Categoria = ProdutoModel.Categoria;

            try
            {

                return RedirectToPage("Produtos");
            }
            catch (HttpRequestException)
            {
                return Page();
            }
        }
    }
}
