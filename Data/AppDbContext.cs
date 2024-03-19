using Bendecido.Models;
using Bendecido.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bendecido.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaSalgado> VendaSalgados { get; set; }
        public DbSet<ResultVenda> ResultVendas { get; set; }

    }    
}