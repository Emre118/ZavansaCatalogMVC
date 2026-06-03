using Microsoft.EntityFrameworkCore;
using ZavansaCatalogMVC.Models;

namespace ZavansaCatalogMVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProductCollection> ProductCollections => Set<ProductCollection>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(product => product.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Product>()
            .HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(product => product.ProductCollection)
            .WithMany(collection => collection.Products)
            .HasForeignKey(product => product.ProductCollectionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ContactMessage>()
            .HasOne(message => message.Product)
            .WithMany()
            .HasForeignKey(message => message.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AdminUser>()
            .HasIndex(user => user.Username)
            .IsUnique();
    }
}
