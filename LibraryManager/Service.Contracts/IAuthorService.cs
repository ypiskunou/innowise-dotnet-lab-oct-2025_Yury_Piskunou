using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync(bool trackChanges);
    Task<AuthorDto> GetAuthorByIdAsync(Guid id, bool trackChanges);
    Task<AuthorDto> AddAuthorAsync(AuthorForCreationDto author);
    Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto author, bool trackChanges);
    Task DeleteAuthorAsync(Guid id, bool trackChanges);
    
    Task<IEnumerable<AuthorDto>> SearchAuthorsByNameAsync(string name, bool trackChanges);
    Task<IEnumerable<AuthorWithBookCountDto>> GetAuthorsWithBookCountsAsync(bool trackChanges);
}