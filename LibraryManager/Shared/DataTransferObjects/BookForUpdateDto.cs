namespace Shared.DataTransferObjects;

public record BookForUpdateDto(string Title, int PublishedYear, Guid AuthorId);