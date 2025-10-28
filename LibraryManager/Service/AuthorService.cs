using Entities;
using Entities.Exceptions;
using LibraryManager.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class AuthorService: IAuthorService
{
    private IRepositoryManager _repository;

    public AuthorService(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public IEnumerable<AuthorDto> GetAllAuthors()
    {
        var authors = _repository.Author.GetAllAuthors();

        return authors.Where(a => a!=null)
            .Select(a => new AuthorDto(a!.Id, a.Name, a.DateOfBirth));
    }

    public AuthorDto GetAuthorById(Guid id)
    {
        var author = _repository.Author.GetAuthorById(id);
        if (author == null)
            throw new AuthorNotFoundException(id);
        return new AuthorDto(author.Id, author.Name, author.DateOfBirth);
    }

    public AuthorDto AddAuthor(AuthorForCreationDto author)
    {
        var authorEntity = new Author
        {
            Name = author.Name, 
            DateOfBirth = author.DateOfBirth
        };
        
        _repository.Author.CreateAuthor(authorEntity);
        
        var authorDto = new AuthorDto(authorEntity.Id, authorEntity.Name, authorEntity.DateOfBirth);
        return authorDto;
    }

    public void UpdateAuthor(Guid id, AuthorForUpdateDto author)
    {
        var authorEntity = _repository.Author.GetAuthorById(id);
        if (authorEntity == null)
            throw new AuthorNotFoundException(id);
        
        authorEntity.Name = author.Name;
        authorEntity.DateOfBirth = author.DateOfBirth;
    }

    public void DeleteAuthor(Guid id)
    {
        var author = _repository.Author.GetAuthorById(id);
        if(author == null) 
            throw new AuthorNotFoundException(id);
        
        _repository.Author.DeleteAuthor(author);
    }
}