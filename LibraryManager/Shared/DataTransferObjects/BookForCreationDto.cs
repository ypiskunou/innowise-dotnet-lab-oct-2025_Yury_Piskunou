namespace Shared.DataTransferObjects;

public record BookForCreationDto(string Title, int PublishedYear, Guid AuthorId);