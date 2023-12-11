using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FrontAceGerenciador2.Pages.Produtos
{
    public class ProdutosModel : PageModel
    {
        public List<ProdutoModel> ProdutoList { get; set; } = new List<ProdutoModel>();

        public ProdutosModel() { }

        public async Task<IActionResult> OnGetAsync()
        {
            using var httpClient = new HttpClient();
            var url = "http://localhost:5151/api/Produtos";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var response = await httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Check if content is valid JSON
                    if (content.StartsWith("{") || content.StartsWith("["))
                    {
                        ProdutoList = JsonConvert.DeserializeObject<List<ProdutoModel>>(content);
                    }
                    else
                    {
                        // Handle non-JSON content (e.g., log or display an error)
                        Console.WriteLine($"Non-JSON content received: {content}");
                        // You might want to throw an exception or set an appropriate error state.
                    }
                }
                else
                {
                    // Handle HTTP error
                    Console.WriteLine($"HTTP error: {response.StatusCode}");
                    // You might want to throw an exception or set an appropriate error state.
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
    }
}
