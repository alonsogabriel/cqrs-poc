using CqrsPoc.WriteApi.Domain;
using CqrsPoc.WriteApi.Domain.Repositories;

namespace CqrsPoc.WriteApi.Infra;

internal class BookCategoryRepository(AppDbContext db) : IBookCategoryRepository
{
    public async Task AddAsync(BookCategory category, CancellationToken ct = default)
    {
        await db.BookCategories.AddAsync(category, ct);
    }

    public async Task<BookCategory?> FindAsync(int categoryId, CancellationToken ct = default)
    {
        return await db.BookCategories.FindAsync([categoryId], ct);
    }
}