using CqrsPoc.ReadApi.App.Models;

namespace CqrsPoc.ReadApi.App.Repositories;

public interface IBookReadRepository
{
    Task<Book?> FindAsync(int productId, CancellationToken ct = default);
}