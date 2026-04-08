using CqrsPoc.Domain.Repositories;

namespace CqrsPoc.Infra;

internal class UnitOfWork(AppWriteDatabase db) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await db.SaveChangesAsync(ct);
    }
}