namespace CqrsPoc.Domain.Repositories;

public interface IBookCategoryRepository
{
    Task<BookCategory?> FindAsync(int categoryId, CancellationToken ct = default);
    Task AddAsync(BookCategory category, CancellationToken ct = default);
}