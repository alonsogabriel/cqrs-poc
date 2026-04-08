namespace CqrsPoc.Domain;

public class Book
{
    private readonly List<Author> _authors = [];
    private Book() { }
    public Book(
        string name,
        IEnumerable<Author> authors,
        BookCategory category,
        Publisher publisher,
        string language,
        string edition,
        int totalPages,
        int? weightGrams)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(category);
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        ArgumentException.ThrowIfNullOrWhiteSpace(edition);
        ArgumentNullException.ThrowIfNull(publisher);

        if (!authors.Any())
            throw new ArgumentException("Book must have at least one author.");

        if (totalPages <= 0)
            throw new ArgumentException("Book must have one or more pages.");

        if (weightGrams is not null && weightGrams.Value <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        Name = name;
        _authors.AddRange(authors);
        Category = category;
        CategoryId = category.Id;
        Publisher = publisher;
        PublisherId = publisher.Id;
        Language = language;
        Edition = edition;
        TotalPages = totalPages;
        WeightGrams = weightGrams;
        CreatedAt = DateTime.UtcNow;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Language { get; private set; }
    public string Edition { get; private set; }
    public int TotalPages { get; private set; }
    public int? WeightGrams { get; private set; }
    public int CategoryId { get; private set; }
    public BookCategory Category { get; private set; }
    public int PublisherId  { get; private set; }
    public Publisher Publisher { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IReadOnlyList<Author> Authors => _authors;
}

public class BookAuthor
{
    public int AuthorId { get; internal set; }
    public int BookId { get; internal set; }
}
