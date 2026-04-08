namespace CqrsPoc.WriteApi.Domain;

public class Book
{
    private Book() { }
    public Book(string name, BookCategory category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(category);

        Name = name;
        Category = category;
        CategoryId = category.Id;
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int CategoryId { get; private set; }
    public BookCategory Category { get; private set; }
}
