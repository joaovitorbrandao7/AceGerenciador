using FrontAceGerenciador.Data;
using FrontAceGerenciador.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FrontAceGerenciador.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Cliente> ClienteList { get; set; } = new(); // Change to List<Cliente>

        public IndexModel(AppDbContext context) // Correct the constructor name to IndexModel
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ClienteList = await _context.Clientes!.ToListAsync();
            return Page();
        }
    }
}
