using CqrsPoc.App;
using CqrsPoc.App.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CqrsPoc.WriteApi;

public static class BooksApi
{
    public static void MapBooksApi(this RouteGroupBuilder builder)
    {
        builder.MapPost("/books", async (
            [FromBody] BookDto data,
            IMediator mediator,
            CancellationToken ct
        ) =>
        {
            var response = await mediator.Handle(data, ct);

            return Results.Ok(response);
        });
    }
}