using Microsoft.EntityFrameworkCore;
using FruitShopMVC.Models;
using FruitShopMVC.DTOs;
namespace FruitShopMVC.Data
{
    public class FruitDbContext : DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ShoppingCarts> Carts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<EmailSubscriptions> EmailSubscribe { get; set; }
        public DbSet<Contacts> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
         .HasOne(od => od.Order)
         .WithMany(o => o.OrderDetails)
         .HasForeignKey(od => od.OrderId)
         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetails>().HasKey(o => o.OrderDetailId);

            modelBuilder.Entity<ProductDTO>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Orders>()
            .Property(o => o.OrderTotal)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetails>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }
    }
}
