namespace CqrsPoc.App.Events;

public class BookCreated
{
    public int Id { get; init; }
    public string Name { get; init; }
    public IEnumerable<string> Authors { get; init; }
    public DateTime CreatedAt { get; init; }

    public class Category
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}