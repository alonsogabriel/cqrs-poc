namespace CqrsPoc.Domain.Repositories;

public interface IPublisherRepository
{
    Task<Publisher?> FindAsync(int publisherId, CancellationToken ct = default);
}