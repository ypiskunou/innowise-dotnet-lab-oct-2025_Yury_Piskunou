using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IBookService
{
    IEnumerable<BookDto> GetAllBooks();
    BookDto GetBookById(Guid id);
    BookDto AddBook(BookForCreationDto book);
    void UpdateBook(Guid id, BookForUpdateDto book);
    void DeleteBook(Guid id);
}