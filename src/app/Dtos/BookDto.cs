using CqrsPoc.App.Models;

namespace CqrsPoc.App.Dtos;

public record BookDto(
    int Id,
    string Name,
    IEnumerable<int> AuthorIds,
    int PublisherId,
    int CategoryId,
    string Edition,
    string Language,
    int TotalPages,
    int? WeightGrams,
    DateTime CreatedAt = default,
    DateTime? UpdatedAt = null)
    : ICommand<BookReadModel>;