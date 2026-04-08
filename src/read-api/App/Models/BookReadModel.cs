using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CqrsPoc.ReadApi.App.Models;

public class Book
{
    public int Id { get; init; }
    // [BsonElement("name")]
    public string Name { get; init; }
    // [BsonElement("authors")]
    public IEnumerable<string> Authors { get; init; }
    // [BsonElement("category")]
    public BookCategory Category { get; init; }
    // [BsonElement("created_at")]
    public DateTime CreatedAt { get; init; }
    // [BsonElement("updated_at")]
    public DateTime? UpdatedAt { get; init; }
}

[BsonNoId]
public class BookCategory
{
    // [BsonElement("id")]
    public int Id { get; init; }
    // [BsonElement("name")]
    public string Name { get; init; }
}