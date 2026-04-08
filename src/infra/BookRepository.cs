using CqrsPoc.Domain;
using CqrsPoc.Domain.Repositories;

namespace CqrsPoc.Infra;

internal class BookRepository(AppWriteDatabase db) : IBookRepository
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