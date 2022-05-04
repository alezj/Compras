using Compras.Datos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Compras.Datos
{
    public class DataContext  : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet <Country> countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c=> c.Name).IsUnique();
        }
    }


}
