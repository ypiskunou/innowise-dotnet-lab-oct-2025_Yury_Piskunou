namespace Entities.Exceptions;

public class AuthorNotFoundException: NotFoundException
{
    public AuthorNotFoundException(Guid id) : 
        base($"Author with id {id} not found")
    {
    }
}