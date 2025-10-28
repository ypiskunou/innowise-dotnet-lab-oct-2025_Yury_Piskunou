using Entities;

namespace LibraryManager.Contracts;

public interface IBookRepository
{
    IEnumerable<Book?> GetAllBooks();
    Book? GetBookById(Guid id);
    void CreateBook(Book book);
    void DeleteBook(Book book);
}