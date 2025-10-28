using Entities;

namespace LibraryManager.Contracts;

public interface IAuthorRepository
{
    IEnumerable<Author?> GetAllAuthors();
    Author? GetAuthorById(Guid id);
    void CreateAuthor(Author author);
    void DeleteAuthor(Author author);
}