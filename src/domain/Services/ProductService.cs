using CqrsPoc.Domain.Repositories;

namespace CqrsPoc.Domain.Services;

public class BookService(
    IBookRepository productRepository,
    IUnitOfWork unitOfWork
)
{
    public async Task CreateAsync(Book product, CancellationToken ct = default)
    {
        await productRepository.AddAsync(product,ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
