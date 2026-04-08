using CqrsPoc.ReadApi.App.Models;
using CqrsPoc.ReadApi.App.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CqrsPoc.ReadApi.Infra.Repositories;

internal class BookReadRepository(IMongoDatabase db) : IBookReadRepository
{
    public async Task<Book?> FindAsync(int productId, CancellationToken ct = default)
    {
        var books = db.GetCollection<Book>("books");
        
        return await books.Find(Builders<Book>.Filter.Eq("_id", productId)).FirstOrDefaultAsync(ct);
    }
}