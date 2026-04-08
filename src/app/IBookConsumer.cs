using CqrsPoc.App.Models;

namespace CqrsPoc.App;

public interface IBookConsumer : IDisposable
{
    Task Init(CancellationToken ct = default);
    Task OnBookCreated(BookReadModel data, CancellationToken ct = default);
}