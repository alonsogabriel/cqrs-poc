using CqrsPoc.Domain;
using CqrsPoc.Domain.Repositories;

namespace CqrsPoc.Infra;

internal class PublisherRepository(
    AppWriteDatabase db
) : IPublisherRepository
{
    public async Task<Publisher?> FindAsync(int publisherId, CancellationToken ct = default)
    {
        return await db.Publishers.FindAsync([publisherId], ct);
    }
}