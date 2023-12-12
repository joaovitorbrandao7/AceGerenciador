using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Clientes
{
    public class EdicaoModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EdicaoModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [BindProperty]
        public ClienteModel ClienteModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = $"http://localhost:5151/api/Clientes/{id}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ClienteModel = JsonConvert.DeserializeObject<ClienteModel>(content);
                }
                else
                {
                    // Handle other status codes if needed
                    ModelState.AddModelError(string.Empty, "Falha ao obter os detalhes do Cliente. Código de status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You might want to throw an exception or set an appropriate error state.
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var url = $"http://localhost:5151/api/Clientes/{ClienteModel.IdCliente}";
                var serializedCliente = JsonConvert.SerializeObject(ClienteModel);
                var content = new StringContent(serializedCliente, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("Clientes");
                }
                else
                {
                    // Log detalhes da resposta para diagnóstico
                    Console.WriteLine($"PUT request failed. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");

                    // Handle other status codes if needed
                    ModelState.AddModelError(string.Empty, "Falha ao atualizar o Cliente. Código de status: " + response.StatusCode);
                    return Page();
                }
            }
            catch (HttpRequestException ex)
            {
                // Log detalhes da exceção para diagnóstico
                Console.WriteLine($"HTTP request failed with exception: {ex.Message}");

                // Handle HttpRequestException
                ModelState.AddModelError(string.Empty, "Erro ao realizar a solicitação HTTP: " + ex.Message);
                return Page();
            }
        }
    }
}
