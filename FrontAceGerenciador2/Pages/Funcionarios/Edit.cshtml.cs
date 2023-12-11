using FrontAceGerenciador2.Data;
using FrontAceGerenciador2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrontAceGerenciador2.Pages.Funcionario
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
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

            FuncionarioModel = await _context.Funcionarios.FirstOrDefaultAsync(m => m.IdFuncionario== id);

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

            var funcionarioToUpdate = await _context.Funcionarios.FindAsync(id);

            if (funcionarioToUpdate == null)
            {
                return NotFound();
            }

            funcionarioToUpdate.NomeFuncionario = FuncionarioModel.NomeFuncionario;
            funcionarioToUpdate.CargoFuncionario = FuncionarioModel.CargoFuncionario;
            funcionarioToUpdate.DataAdmissao = FuncionarioModel.DataAdmissao;
           

            try
            {
                var httpClient = new HttpClient();
                var url = $"http://localhost:5151/api/Funcionarios/{id}"; // Substitua isso pela URL correta da sua API
                var serializedFuncionario = JsonConvert.SerializeObject(FuncionarioModel);
                var content = new StringContent(serializedFuncionario, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    await _context.SaveChangesAsync();
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
