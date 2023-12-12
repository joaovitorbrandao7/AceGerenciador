using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Funcionario
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public FuncionarioModel FuncionarioModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Funcionarios/{id}";
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var httpClient = new HttpClient();
                var url = $"http://localhost:5151/api/Funcionarios/{id}";
                var serializedFuncionario = JsonConvert.SerializeObject(FuncionarioModel);
                var content = new StringContent(serializedFuncionario, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("Funcionarios");
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
