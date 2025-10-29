using System.Linq.Expressions;
using Entities;

namespace LibraryManager.Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author?>> GetAllAuthorsAsync(bool trackChanges);
    Task<Author?> GetAuthorByIdAsync(Guid id, bool trackChanges);
    void CreateAuthor(Author author);
    void DeleteAuthor(Author author);
    
    Task<IEnumerable<Author?>> SearchAuthorsByNameAsync(string name, bool trackChanges);
    
    IQueryable<Author?> GetAuthorsWithBooks(bool trackChanges);
    Task<IEnumerable<T>> GetAuthorsAsAsync<T>(IQueryable<Author?> authors, Expression<Func<Author, T>> selector);
    
}