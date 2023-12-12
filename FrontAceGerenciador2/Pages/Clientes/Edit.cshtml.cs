using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Clientes
{
    public class EditModel : PageModel
    {
   
        [BindProperty]
        public ClienteModel ClienteModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = new HttpClient();
            var url = $"http://localhost:5151/api/Clientes/{id}"; 
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            ClienteModel = JsonConvert.DeserializeObject<ClienteModel>(content);

            if (ClienteModel == null)
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
            var url = $"http://localhost:5151/api/Clientes/{id}"; 
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            var clienteToUpdate = JsonConvert.DeserializeObject<ClienteModel>(content);

            clienteToUpdate.NomeCliente = ClienteModel.NomeCliente;
            clienteToUpdate.EmailCliente = ClienteModel.EmailCliente;
            clienteToUpdate.TelefoneCliente = ClienteModel.TelefoneCliente;
            clienteToUpdate.EnderecoCliente = ClienteModel.EnderecoCliente;
            clienteToUpdate.TipoPet = ClienteModel.TipoPet;

            try
            {
                // Substitua as linhas abaixo para fazer o PUT na API
                var serializedCliente = JsonConvert.SerializeObject(clienteToUpdate);
                var putUrl = $"http://localhost:5151/api/Clientes/{id}"; // Substitua isso pela URL correta da sua API
                var putContent = new StringContent(serializedCliente, Encoding.UTF8, "application/json");

                var putResponse = await httpClient.PutAsync(putUrl, putContent);

                if (putResponse.IsSuccessStatusCode)
                {
                    return RedirectToPage("Clientes");
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
