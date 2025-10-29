using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges);
    Task<BookDto> GetBookByIdAsync(Guid id, bool trackChanges);
    Task<BookDto> AddBookAsync(BookForCreationDto book);
    Task UpdateBookAsync(Guid id, BookForUpdateDto book, bool trackChanges);
    Task DeleteBookAsync(Guid id, bool trackChanges);
    
    Task<IEnumerable<BookDto>> GetBooksPublishedAfterAsync(int year, bool trackChanges);
}