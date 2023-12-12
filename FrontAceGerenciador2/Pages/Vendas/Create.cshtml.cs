using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FrontAceGerenciador2.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontAceGerenciador2.Pages.Vendas
{
    public class CreateModel : PageModel
    {
        private readonly string apiUrl = "http://localhost:5151/";

        public List<VendaInputModel> VendaList { get; set; } = new();
        public List<ProdutoModel> ProdutoList { get; set; } = new();

        public CreateModel() { }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDataAsync();
            return Page();
        }

        [BindProperty]
        public VendaInputModel VendaModel { get; set; }

        // Adicionado para preencher a lista de produtos no dropdown
        [BindProperty]
        public int ProdutoId { get; set; }

        [BindProperty]
        public int Quantidade { get; set; }

        // Adicionado para preencher a lista de produtos no dropdown
        public SelectList ProdutoSelectList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Cria um novo ProdutoVendidoModel a partir dos dados do formulário
                var produtoVendido = new ProdutoVendidoModel
                {
                    ProdutoId = ProdutoId,
                    Quantidade = Quantidade
                };

                // Adiciona o produto vendido à lista existente
                VendaModel.ProdutosVendidos.Add(produtoVendido);

                try
                {
                    var httpClient = new HttpClient();
                    var url = "http://localhost:5151/api/Vendas";
                    var serializedCliente = JsonConvert.SerializeObject(VendaModel);
                    var content = new StringContent(serializedCliente, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Você pode redirecionar para a página de listagem ou fazer algo mais
                        return RedirectToPage("Vendas");
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

            await LoadDataAsync();
            return Page();
        }

        private async Task LoadDataAsync()
        {
            var httpClient = new HttpClient();

            // Carrega a lista de vendas
            var vendaResponse = await httpClient.GetAsync(apiUrl);
            var vendaContent = await vendaResponse.Content.ReadAsStringAsync();
            VendaList = JsonConvert.DeserializeObject<List<VendaInputModel>>(vendaContent);

            // Carrega a lista de produtos para preencher o dropdown
            var produtoResponse = await httpClient.GetAsync(apiUrl + "api/Produtos");
            var produtoContent = await produtoResponse.Content.ReadAsStringAsync();
            ProdutoList = JsonConvert.DeserializeObject<List<ProdutoModel>>(produtoContent);

            // Preenche o SelectList para o dropdown de produtos
            ProdutoSelectList = new SelectList(ProdutoList, nameof(ProdutoModel.ProdutoId), nameof(ProdutoModel.Nome));
        }
    }
}
