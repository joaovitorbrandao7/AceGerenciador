using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FrontAceGerenciador2.Data;

namespace FrontAceGerenciador2.Pages.Funcionario;

public class CreateModel : PageModel
{
    public List<FuncionariosModel> FuncionarioList { get; set; } = new();

    public CreateModel() { }

    public async Task<IActionResult> OnGetAsync()
    {
        var httpClient = new HttpClient();
        var url = "http://localhost:5151/";
        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        FuncionarioList = JsonConvert.DeserializeObject<List<FuncionariosModel>>(content!);

        return Page();
    }

    [BindProperty]
    public FuncionarioModel FuncionarioModel { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:5151/api/Funcionarios";
            var serializedFuncionario = JsonConvert.SerializeObject(FuncionarioModel);
            var content = new StringContent(serializedFuncionario, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to the page showing the list of Funcionarios
                return RedirectToPage("Funcionarios/Index");
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