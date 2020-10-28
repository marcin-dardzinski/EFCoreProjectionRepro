using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFProjectionRepro
{
    public class Context : DbContext
    {
        public DbSet<Entity> Entities => Set<Entity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=App;User Id=sa;Password=Password1!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>(cfg =>
            {
                cfg.OwnsMany(e => e.Children, inner =>
                {
                    inner.OwnsMany(e => e.Owned);
                });
            });
        }
    }

    public class Entity
    {
        public int Id { get; set; }
        public List<Child> Children { get; set; }
    }

    public class Child
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public List<Owned> Owned { get; set; }
    }

    public class Owned
    {
        public string Value { get; set; }
    }
}