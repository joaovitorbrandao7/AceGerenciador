using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontAceGerenciador2.Pages.Funcionario
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public FuncionarioModel FuncionarioModel { get; set; } = new();

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
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = $"http://localhost:5151/api/Funcionarios/{id}";
                    var response = await httpClient.DeleteAsync(url);

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
            }
            catch (HttpRequestException)
            {
                // Lógica de tratamento de erro, se necessário
                return Page();
            }
        }
    }
}
