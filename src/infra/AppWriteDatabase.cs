using CqrsPoc.Domain;
using CqrsPoc.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CqrsPoc.Infra;

internal class AppWriteDatabase(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Book> Books { get; init; }
    public DbSet<Author> Authors { get; init; }
    public DbSet<Publisher> Publishers { get; init; }
    public DbSet<BookCategory> BookCategories { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new BookCategoryMap())
            .ApplyConfiguration(new BookMap())
            .ApplyConfiguration(new AuthorMap())
            .ApplyConfiguration(new PublisherMap());
    }
}