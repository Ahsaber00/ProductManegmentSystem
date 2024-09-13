using Microsoft.EntityFrameworkCore;
using ProductManegmentSystem.Models;

namespace ProductManegmentSystem.Context
{
    public class ProductManegmentContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=LAPTOP-LVDH695F;DataBase=ProductMis;Trusted_Connection=True;Encrypt=false");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", Description = "Devices, gadgets, and accessories" },
                new Category { Id = 2, Name = "Books", Description = "Books of all genres and formats" },
                new Category { Id = 3, Name = "Home & Kitchen", Description = "Home appliances and kitchen tools" },
                new Category { Id = 4, Name = "Clothing", Description = "Men's, Women's, and Children's clothing" },
                new Category { Id = 5, Name = "Sports & Outdoors", Description = "Equipment and gear for outdoor activities" }
            };
            var products = new List<Product>
            {
                // Electronics
                new Product {Id=1, Title = "Smartphone", Description = "Latest 5G smartphone", Price = 799.99m, Quantity = 50, ImageFileName = "1-2-samsung-mobile-phone-free-png-image.png", CategoryId = 1 },
                new Product {Id=2, Title = "Wireless Earbuds", Description = "Noise-cancelling earbuds", Price = 149.99m, Quantity = 150, ImageFileName = "airpods.JPEG", CategoryId = 1 },
                
                // Books
                new Product {Id=3, Title = "The Great Gatsby", Description = "Classic novel by F. Scott Fitzgerald", Price = 10.99m, Quantity = 100, ImageFileName = "book.jpeg", CategoryId = 2 },
                new Product {Id=4, Title = "Educated", Description = "A Memoir by Tara Westover", Price = 18.99m, Quantity = 60, ImageFileName = "book2.jpeg", CategoryId = 2 },
                
                // Home & Kitchen
                new Product {Id=5 ,Title = "Blender", Description = "High-speed blender for smoothies", Price = 79.99m, Quantity = 30, ImageFileName = "blender.jpeg", CategoryId = 3 },
                new Product {Id=6, Title = "Coffee Maker", Description = "Automatic drip coffee machine", Price = 49.99m, Quantity = 40, ImageFileName = "cofeemaker.jpeg", CategoryId = 3 },
                
                // Clothing
                new Product {Id=7, Title = "Men's T-Shirt", Description = "Cotton crewneck t-shirt", Price = 12.99m, Quantity = 200, ImageFileName = "menTshirt.jpeg", CategoryId = 4 },
                new Product {Id=8, Title = "Women's Jeans", Description = "Skinny fit jeans for women", Price = 49.99m, Quantity = 120, ImageFileName = "womenjeans.jpeg", CategoryId = 4 },

                // Sports & Outdoors
                new Product {Id=9, Title = "Yoga Mat", Description = "Eco-friendly non-slip yoga mat", Price = 24.99m, Quantity = 100, ImageFileName = "YogaMat.jpeg", CategoryId = 5 },
                new Product {Id=10, Title = "Tennis Racket", Description = "Professional-grade tennis racket", Price = 119.99m, Quantity = 50, ImageFileName = "racket.jpeg", CategoryId = 5 }
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }  
    }
}
