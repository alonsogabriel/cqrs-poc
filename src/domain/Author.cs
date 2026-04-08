using System.ComponentModel;

namespace CqrsPoc.Domain;

public enum Gender { Male, Female }

public class Author
{
    private readonly List<Book> _books = [];
    private Author() { }
    public Author(
        string name,
        Gender gender,
        string nationality,
        DateOnly? birthDate,
        DateOnly? deathDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!Enum.IsDefined(gender))
            throw new InvalidEnumArgumentException("Invalid gender.");

        Name = name;
        Gender = gender;
        Nationality = nationality;
        BirthDate = birthDate;
        DeathDate = deathDate;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public Gender Gender { get; private set; }
    public string Nationality { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public DateOnly? DeathDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyList<Book> Books => _books;
}