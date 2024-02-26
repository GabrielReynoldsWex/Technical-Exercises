using MediatrExercisev2.Domain.Entities.CustomerClass;
using MediatrExercisev2.Domain.Entities.ItemClass;
using MediatrExercisev2.Domain.Entities.PurchaseClass;
using Microsoft.EntityFrameworkCore;

namespace MediatrExercisev2.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Purchase> Purchases { get; set;}
    }
}