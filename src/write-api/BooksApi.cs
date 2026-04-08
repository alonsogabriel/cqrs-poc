using CqrsPoc.App.Events;
using CqrsPoc.WriteApi.App;
using CqrsPoc.WriteApi.Domain;
using CqrsPoc.WriteApi.Domain.Repositories;
using CqrsPoc.WriteApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CqrsPoc.WriteApi;

public static class BooksApi
{
    public static void MapBooksApi(this RouteGroupBuilder builder)
    {
        builder.MapPost("/books", async (
            [FromBody] ProductDto data,
            BookService service,
            IBookCategoryRepository categoryRepository,
            IBookProducer producer,
            CancellationToken ct
        ) =>
        {
            var category = await categoryRepository.FindAsync(data.CategoryId, ct);

            if (category is null)
                return Results.BadRequest(new { message = "Category not found." });

            var book = new Book(data.Name, category);
            await service.CreateAsync(book, ct);

            var ev = new BookCreated
            {
                Id = book.Id,
                Name = book.Name,
                Authors = ["Gabriel Alonso"],
                CreatedAt = DateTime.UtcNow
            };

            await producer.SendAsync(ev, ct);

            return Results.Ok(new { productId = book.Id });
        });
    }
}

public record ProductDto(int Id, string Name, int CategoryId);