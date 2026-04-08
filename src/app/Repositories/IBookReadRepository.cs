using CqrsPoc.App.Models;

namespace CqrsPoc.App.Repositories;

public interface IBookReadRepository
{
    Task<BookReadModel?> FindAsync(int productId, CancellationToken ct = default);
}