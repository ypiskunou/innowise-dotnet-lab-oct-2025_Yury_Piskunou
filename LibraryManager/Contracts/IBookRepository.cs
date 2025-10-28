using Entities;

namespace LibraryManager.Contracts;

public interface IBookRepository
{
    Task<IEnumerable<Book?>> GetAllBooksAsync(bool trackChanges);
    Task<Book?> GetBookByIdAsync(Guid id, bool trackChanges);
    void CreateBook(Book book);
    void DeleteBook(Book book);
}