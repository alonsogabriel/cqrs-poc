using CqrsPoc.WriteApi.Domain.Repositories;

namespace CqrsPoc.WriteApi.Infra;

internal class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await db.SaveChangesAsync(ct);
    }
}