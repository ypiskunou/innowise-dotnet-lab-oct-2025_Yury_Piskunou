namespace Entities.Exceptions;

public class BookNotFoundException: NotFoundException
{
    public BookNotFoundException(Guid id) :
        base($"Book with id {id} not found")
    {
    }
}