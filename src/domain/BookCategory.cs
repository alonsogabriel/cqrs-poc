namespace CqrsPoc.Domain;

public class BookCategory
{
    private readonly List<BookCategory> _children = [];
    private BookCategory() { }
    public BookCategory(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int? ParentId { get; private set; }
    public IReadOnlyList<BookCategory> Children => _children;
}
