using CqrsPoc.Domain;
using CqrsPoc.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CqrsPoc.Infra;

internal class AuthorRepository(AppWriteDatabase db) : IAuthorRepository
{
    public async Task<Author?> FindAsync(int authorId, CancellationToken ct = default)
    {
        return await db.Authors.FindAsync([authorId], ct);
    }

    public async Task<IEnumerable<Author>> FindByIdsAsync(int[] authorIds, CancellationToken ct = default)
    {
        return await db.Authors
            .Where(a => authorIds.Contains(a.Id))
            .ToListAsync(ct);
    }
}