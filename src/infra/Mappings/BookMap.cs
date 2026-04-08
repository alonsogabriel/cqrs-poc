using CqrsPoc.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CqrsPoc.Infra.Mappings;

internal class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(nameof(Book));

        builder.HasKey(b => b.Id);

        builder.HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<BookAuthor>(nameof(BookAuthor),
                r => r.HasOne<Author>().WithMany().HasForeignKey(e => e.AuthorId).IsRequired(),
                l => l.HasOne<Book>().WithMany().HasForeignKey(e => e.BookId).IsRequired(),
                b => b.HasKey(e => new { e.AuthorId, e.BookId }));

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .IsRequired();

        builder.HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .IsRequired();

        builder.Property(b => b.TotalPages)
            .IsRequired();

        builder.Property(b => b.Edition)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.Language)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(b => b.WeightGrams)
            .IsRequired(false);

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        builder.Property(b => b.UpdatedAt)
            .IsRequired(false);
    }
}