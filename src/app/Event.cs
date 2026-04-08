namespace CqrsPoc.App;

public class Event<T>(string name, T? data = null)
    where T : class
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public string Name => name;
    public T? Data => data;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}

public static class Event
{
    public static Event<T> Create<T>(string name, T? data = null)
        where T : class
    {
        return new(name, data);
    }
}