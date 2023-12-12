using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Produtos
{
    public class DetailsModel : PageModel
    {


        [BindProperty]
        public ProdutoModel ProdutoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar os detalhes do Funcionario da sua API
            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Produtos/{id}"; // Substitua isso pela URL correta da sua API
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
    }
}
