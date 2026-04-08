namespace CqrsPoc.Domain.Repositories;

public interface IAuthorRepository
{
    Task<Author?> FindAsync(int authorId, CancellationToken ct = default);
    Task<IEnumerable<Author>> FindByIdsAsync(int[] authorIds, CancellationToken ct = default);
}