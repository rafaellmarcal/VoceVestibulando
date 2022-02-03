using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VV.Catalogo.API.Models;
using VV.Core.Data;

namespace VV.Catalogo.API.Data
{
    public class CatalogoDbContext : DbContext, IUnitOfWork
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
