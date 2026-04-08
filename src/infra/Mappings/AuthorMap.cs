using CqrsPoc.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CqrsPoc.Infra.Mappings;

internal class AuthorMap : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable(nameof(Author));

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.Nationality)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(a => a.Gender)
            .IsRequired()
            .HasConversion(
                value => Enum.GetName(value)!,
                value => Enum.Parse<Gender>(value, true));

        builder.Property(a => a.BirthDate)
            .IsRequired(false);

        builder.Property(a => a.DeathDate)
            .IsRequired(false);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired(false);
    }
}