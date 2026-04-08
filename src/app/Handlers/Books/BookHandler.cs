using CqrsPoc.App.Dtos;
using CqrsPoc.App.Models;
using CqrsPoc.Domain;
using CqrsPoc.Domain.Repositories;
using CqrsPoc.Domain.Services;
using CqrsPoc.WriteApi.App;

namespace CqrsPoc.App.Handlers.Books;

public class BookHandler(
    BookService bookService,
    IBookCategoryRepository categoryRepository,
    IAuthorRepository authorRepository,
    IPublisherRepository publisherRepository,
    IBookProducer producer
) :
    ICommandHandler<BookDto, BookReadModel>
{
    public async Task<BookReadModel> Handle(BookDto command, CancellationToken ct = default)
    {
        var category = await categoryRepository.FindAsync(command.CategoryId, ct);

        // TODO replace with result pattern
        if (category is null)
            throw new Exception("Category not found.");

        var authors = await authorRepository.FindByIdsAsync([.. command.AuthorIds], ct);

        if (authors.Count() != command.AuthorIds.Count())
            throw new Exception("Author not found.");

        var publisher = await publisherRepository.FindAsync(command.PublisherId, ct);

        if (publisher is null)
            throw new Exception("Publisher not found.");

        var book = new Book(
            name: command.Name,
            authors: authors!,
            category: category,
            publisher: publisher,
            language: command.Language,
            edition: command.Edition,
            totalPages: command.TotalPages,
            weightGrams: command.WeightGrams
        );

        await bookService.CreateAsync(book, ct);
        var response = BookReadModel.From(book);

        var @event = Event.Create("BookCreated", response);

        await producer.SendAsync(@event, ct);

        return response;
    }
}