using Microsoft.EntityFrameworkCore;
using WebFruit.Models;
namespace WebFruit.Data
{
    public class FruitDbContext : DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EmailSubscriptions> EmailSubscriptions { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<Comment> Comments { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Category
            builder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            // Comment
            builder.Entity<Comment>()
                .HasKey(c => c.CommentId);
            builder.Entity<Comment>()
                .HasOne(c => c.New)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.NewId);

            // Contact
            builder.Entity<Contact>()
                .HasKey(c => c.ContactId);

            // EmailSubscriptions
            builder.Entity<EmailSubscriptions>()
                .HasKey(e => e.Id);

            // New
            builder.Entity<New>()
                .HasKey(n => n.NewId);

            // Order
            builder.Entity<Order>()
                .HasKey(o => o.OrderId);

            // OrderDetail
            builder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailId);
            builder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
            builder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            // Product
            builder.Entity<Product>()
                .HasKey(p => p.ProductId);
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            // ShoppingCart
            builder.Entity<ShoppingCart>()
                .HasKey(sc => sc.Id);
            builder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Product)
                .WithMany()
                .HasForeignKey(sc => sc.ProductId);
        }
    }
}
