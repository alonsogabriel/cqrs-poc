using CqrsPoc.App.Models;
using CqrsPoc.App.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CqrsPoc.Infra;

internal class BookReadRepository(IMongoDatabase db) : IBookReadRepository
{
    public async Task<BookReadModel?> FindAsync(int productId, CancellationToken ct = default)
    {
        var books = db.GetCollection<BookReadModel>("books");
        
        return await books.Find(Builders<BookReadModel>.Filter.Eq("_id", productId)).FirstOrDefaultAsync(ct);
    }
}