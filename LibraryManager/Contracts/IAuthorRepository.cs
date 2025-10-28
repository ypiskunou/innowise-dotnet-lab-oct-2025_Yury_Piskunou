using Entities;

namespace LibraryManager.Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author?>> GetAllAuthorsAsync(bool trackChanges);
    Task<Author?> GetAuthorByIdAsync(Guid id, bool trackChanges);
    void CreateAuthor(Author author);
    void DeleteAuthor(Author author);
}