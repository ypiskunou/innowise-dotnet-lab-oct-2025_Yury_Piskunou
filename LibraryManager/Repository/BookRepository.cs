using Entities;
using LibraryManager.Contracts;

namespace Repository;

public class BookRepository: IBookRepository
{
    public IEnumerable<Book?> GetAllBooks() => InMemoryDataStorage.Books.ToList();

    public Book? GetBookById(Guid id) => InMemoryDataStorage.Books
        .FirstOrDefault(b => b != null && b.Id == id);

    public void CreateBook(Book book) 
    {
        book.Id = Guid.NewGuid();
        InMemoryDataStorage.Books.Add(book);
    }

    public void DeleteBook(Book book) => InMemoryDataStorage.Books.Remove(book);
}