using Microsoft.EntityFrameworkCore;
using RestApiDDD.Domain.Entities;

namespace RestApiDDD.Infrastructure.Data
{
    public class ConnectionContext : DbContext
    { 
        public ConnectionContext() { } 
        public ConnectionContext(DbContextOptions<ConnectionContext> option) : base(option) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegistrationDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegistrationDate").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}