

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
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Commnet> Commnets { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasData(new Admin() { Id = Guid.NewGuid() ,Username="admin",Password="123456",Name="Admin"});
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
            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(x => x.Food)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(x => x.Restaurant)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Food>(entity =>
            {
                entity
                .HasOne(x => x.Category)
                .WithMany(x=>x.Foods)
                .HasForeignKey(x => x.CategoryId) 
                .OnDelete(DeleteBehavior.Restrict);

                entity
                .HasOne(x => x.Restaurant)
                .WithMany(x => x.Foods)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Commnet>(entity =>
            {
                entity
                    .HasOne(x => x.Food)
                    .WithMany(x => x.Commnets)
                    .HasForeignKey(x => x.FoodId)
                     .OnDelete(DeleteBehavior.SetNull);
                entity
                .HasOne(x => x.ReplayTo)
                .WithMany(x => x.Replays)
                .HasForeignKey(x => x.ReplayToId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                    .HasOne(x => x.Client)
                    .WithMany(x => x.Commnets)
                    .HasForeignKey(x => x.ClientId);
            });
            modelBuilder.Entity<Restaurant>(entity => {
                entity.HasMany(x => x.Reports)
                    .WithOne(x => x.Restaurant)
                    .HasForeignKey(x => x.RestaurantId);
            });
            modelBuilder.Entity<Report>(entity =>
            {
                entity
                .HasOne(x => x.Restaurant)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasOne(x => x.Client)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Rating>(entity =>
            {
                entity
                .HasOne(x => x.Food)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasOne(x=>x.Client)
                .WithMany(x=>x.Ratings)
                .HasForeignKey(x=>x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Food>().ToTable("Foods");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Commnet>().ToTable("Comments");
            modelBuilder.Entity<Report>().ToTable("Reports");
            modelBuilder.Entity<Rating>().ToTable("Ratings");
        }
    }
}
