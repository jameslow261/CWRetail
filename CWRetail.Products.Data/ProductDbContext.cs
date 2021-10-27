using CWRetail.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace CWRetail.Products.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => 
            options.UseSqlServer("Server=tcp:cwretail.database.windows.net,1433;Initial Catalog=Products;Persist Security Info=False;User ID=cwretail;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new Product[]
            {
                new Product{ Id = 1, Name = "Book product", Type = Type.Books, Price = 1.23M, IsActive = true },
                new Product{ Id = 2, Name = "Electronics product", Type = Type.Electronics, Price = 2.34M, IsActive = true },
                new Product{ Id = 3, Name = "Food product", Type = Type.Food, Price = 3.45M, IsActive = true },
                new Product{ Id = 4, Name = "Furniture product", Type = Type.Furniture, Price = 4.56M, IsActive = true },
                new Product{ Id = 5, Name = "Toys product", Type = Type.Toys, Price = 5.67M, IsActive = true }
            };

            modelBuilder.Entity<Product>().HasData(products);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        
    }
}
