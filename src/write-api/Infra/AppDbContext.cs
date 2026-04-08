using CqrsPoc.WriteApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace CqrsPoc.WriteApi.Infra;

internal class AppDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Book> Books { get; init; }
    public DbSet<BookCategory> BookCategories { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var product = modelBuilder.Entity<Book>();

        product.HasKey(p => p.Id);

        product.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        product.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();

        var productCategory = modelBuilder.Entity<BookCategory>();

        productCategory.HasKey(c => c.Id);

        productCategory.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsRequired();

        productCategory.HasMany(c => c.Children)
            .WithOne()
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);
    }
}