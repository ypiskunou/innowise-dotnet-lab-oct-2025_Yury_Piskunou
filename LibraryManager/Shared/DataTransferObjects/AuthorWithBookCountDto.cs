namespace Shared.DataTransferObjects;

public record AuthorWithBookCountDto(Guid Id, string Name, int? BookCount);