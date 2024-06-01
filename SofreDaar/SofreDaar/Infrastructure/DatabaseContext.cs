

using Microsoft.EntityFrameworkCore;
using SofreDaar.Models;

namespace SofreDaar.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity
                .HasOne(x => x.Restaurant)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasOne(x => x.Client)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasMany(x => x.Foods)
                .WithMany(x => x.Orders);
            });
            modelBuilder.Entity<Food>(entity =>
            {
                entity
                .HasOne(x => x.Restaurant)
                .WithMany(x => x.Foods)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Food>().ToTable("Foods");
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }
}
