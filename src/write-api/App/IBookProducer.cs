namespace CqrsPoc.WriteApi.App;

public interface IBookProducer
{
    Task<bool> SendAsync(object message, CancellationToken ct = default);
}