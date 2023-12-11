using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Funcionario
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FuncionarioModel FuncionarioModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar os detalhes do Funcionario da sua API
            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Funcionarios/{id}"; // Substitua isso pela URL correta da sua API
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            FuncionarioModel = JsonConvert.DeserializeObject<FuncionarioModel>(content);

            if (FuncionarioModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
