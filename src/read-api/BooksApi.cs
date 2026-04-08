using CqrsPoc.ReadApi.App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CqrsPoc.ReadApi;

public static class BooksApi
{
    public static void MapBooksApi(this RouteGroupBuilder builder)
    {
        builder.MapGet("/books/{bookId}", async (
            [FromRoute] int bookId,
            [FromServices] IBookReadRepository repository,
            CancellationToken ct 
        ) =>
        {
            var book = await repository.FindAsync(bookId, ct);

            if (book is null)
                return Results.NotFound();

            return Results.Ok(book);
        });
    }
}