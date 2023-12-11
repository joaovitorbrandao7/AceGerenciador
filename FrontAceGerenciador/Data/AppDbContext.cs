using FrontAceGerenciador.Pages.Shared;
using Microsoft.EntityFrameworkCore;

namespace FrontAceGerenciador.Data
{
    // Change DbSet type from ClientesModel to Cliente
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=tds.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
        }
    }

}
