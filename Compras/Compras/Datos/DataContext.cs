using Compras.Datos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Compras.Datos
{
    public class DataContext  : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet <Country> countries { get; set; }

        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c=> c.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name","StateID").IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name","CountryID").IsUnique();
        }
    }


}
