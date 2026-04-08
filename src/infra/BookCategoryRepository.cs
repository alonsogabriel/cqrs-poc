using CqrsPoc.Domain;
using CqrsPoc.Domain.Repositories;

namespace CqrsPoc.Infra;

internal class BookCategoryRepository(AppWriteDatabase db) : IBookCategoryRepository
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