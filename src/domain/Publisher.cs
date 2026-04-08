namespace CqrsPoc.Domain;

public class Publisher
{
    private readonly List<Book> _books = [];
    private Publisher() { }
    public Publisher(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        CreatedAt = DateTime.UtcNow;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyList<Book> Books => _books;
}