namespace CqrsPoc.Domain.Repositories;

public interface IBookRepository
{
    Task<Book?> FindAsync(int bookId, CancellationToken ct = default);
    Task AddAsync(Book book, CancellationToken ct = default);
}
