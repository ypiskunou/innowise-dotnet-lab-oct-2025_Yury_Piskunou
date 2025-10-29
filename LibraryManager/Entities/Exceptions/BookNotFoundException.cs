namespace Entities.Exceptions;

public sealed class BookNotFoundException: NotFoundException
{
    public BookNotFoundException(Guid id) :
        base($"Book with id {id} not found")
    {
    }
}