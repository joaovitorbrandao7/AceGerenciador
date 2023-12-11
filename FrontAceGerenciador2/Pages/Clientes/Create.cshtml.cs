using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FrontAceGerenciador2.Data;

namespace FrontAceGerenciador2.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public List<ClienteModel> ClienteList { get; set; } = new();

        public CreateModel() { }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:5151/";
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            ClienteList = JsonConvert.DeserializeObject<List<ClienteModel>>(content!);

            return Page();
        }

        [BindProperty]
        public ClienteModel ClienteModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var httpClient = new HttpClient();
                var url = "http://localhost:5151/api/Clientes";
                var serializedCliente = JsonConvert.SerializeObject(ClienteModel);
                var content = new StringContent(serializedCliente, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // Você pode redirecionar para a página de listagem ou fazer algo mais
                    return RedirectToPage("Clientes");
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
