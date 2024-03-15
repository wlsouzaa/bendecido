using Bendecido.Models;
using Bendecido.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bendecido.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaSalgado> VendaSalgados { get; set; }
        public DbSet<ResultVenda> ResultVendas { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=db_Bendecido;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
    }    
}