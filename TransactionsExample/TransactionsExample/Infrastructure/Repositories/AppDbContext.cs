using Microsoft.EntityFrameworkCore;
using TransactionsExample.Domain;

namespace TransactionsExample.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Description = "Gaming laptop", Stock = 10, Price = 1200 },
            new Product { Id = 2, Name = "Monitor", Description = "4K Monitor", Stock = 20, Price = 300 },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Stock = 50, Price = 80 },
            new Product { Id = 4, Name = "Mouse", Description = "Wireless mouse", Stock = 40, Price = 60 },
            new Product { Id = 5, Name = "Headphones", Description = "Noise cancelling", Stock = 25, Price = 150 }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CustomerName = "Alice", CreatedAt = DateTime.UtcNow, Count = 2, TotalPrice = 2400, ProductId = 1 },
            new Order { Id = 2, CustomerName = "Bob", CreatedAt = DateTime.UtcNow, Count = 1, TotalPrice = 300, ProductId = 2 },
            new Order { Id = 3, CustomerName = "Charlie", CreatedAt = DateTime.UtcNow, Count = 3, TotalPrice = 240, ProductId = 3 }
        );
    }
}