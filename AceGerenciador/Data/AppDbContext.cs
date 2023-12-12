// AppDbContext.cs
using AceGerenciador.Models;
using Microsoft.EntityFrameworkCore;

namespace AceGerenciador.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<RelatorioDiario> RelatorioDiario { get; set; }
        public DbSet<Venda> Venda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<Compra>().HasKey(d => d.IdCompra);
            modelBuilder.Entity<Funcionario>().HasKey(f => f.IdFuncionario);
            modelBuilder.Entity<Produto>().HasKey(p => p.ProdutoId);
            modelBuilder.Entity<RelatorioDiario>().HasKey(r => r.IdRelatorio);
            modelBuilder.Entity<Venda>().HasKey(v => v.VendaId);
            modelBuilder.Entity<ProdutoVendido>()
    .HasKey(pv => new { pv.ProdutoId, pv.VendaId, pv.FuncionarioId });


            modelBuilder.Entity<Venda>()
                .HasMany(v => v.ProdutosVendidos)
                .WithOne(pv => pv.Venda)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public class SeuServico
        {
            private readonly AppDbContext _dbContext;

            public SeuServico(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public Venda ObterDetalhesDaVenda(int vendaId)
            {
                var vendaComDetalhes = _dbContext.Venda
                    .Include(v => v.ProdutosVendidos)
                        .ThenInclude(pv => pv.Produto) 
                    .Include(v => v.Clientes) 
                    .Include(v => v.Funcionario) 
                    .FirstOrDefault(v => v.VendaId == vendaId);

                return vendaComDetalhes;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=tds.db;Cache=Shared");
    }
}

