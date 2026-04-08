using CqrsPoc.WriteApi.Domain;
using CqrsPoc.WriteApi.Domain.Repositories;

namespace CqrsPoc.WriteApi.Infra;

internal class BookRepository(AppDbContext db) : IBookRepository
{
    public async Task AddAsync(Book book, CancellationToken ct = default)
    {
        await db.Books.AddAsync(book, ct);
    }

    public async Task<Book?> FindAsync(int bookId, CancellationToken ct = default)
    {
        return await db.Books.FindAsync([bookId], ct);
    }
}