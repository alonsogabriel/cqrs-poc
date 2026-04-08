using CqrsPoc.Domain;

namespace CqrsPoc.App.Models;

public class BookReadModel
{
    public static BookReadModel From(Book book)
    {
        return new()
        {
            Id = book.Id,
            Name = book.Name,
            Authors = book.Authors.Select(a => new AuthorData()
            {
                Id = a.Id,
                Name = a.Name
            }),
            Publisher = new()
            {
                Id = book.PublisherId,
                Name = book.Publisher.Name
            },
            Category = new()
            {
                Id = book.CategoryId,
                Name = book.Category.Name
            },
            Language = book.Language,
            Edition = book.Edition,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
    public int Id { get; init; }
    // [BsonElement("name")]
    public string Name { get; init; }
    // [BsonElement("authors")]
    public IEnumerable<AuthorData> Authors { get; init; }
    public PublisherData Publisher { get; init; }
    // [BsonElement("category")]
    public CategoryData Category { get; init; }
    public string Language { get; init; }
    public string Edition { get; init; }
    // [BsonElement("created_at")]
    public DateTime CreatedAt { get; init; }
    // [BsonElement("updated_at")]
    public DateTime? UpdatedAt { get; init; }

    public class AuthorData
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    // [BsonNoId]
    public class CategoryData
    {
        // [BsonElement("id")]
        public int Id { get; init; }
        // [BsonElement("name")]
        public string Name { get; init; }
    }

    public class PublisherData
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}