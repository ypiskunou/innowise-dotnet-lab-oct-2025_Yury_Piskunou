using Entities;
using Entities.Exceptions;
using LibraryManager.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class BookService: IBookService
{
    private IRepositoryManager _repository;

    public BookService(IRepositoryManager repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<BookDto> GetAllBooks()
    {
        var books = _repository.Book.GetAllBooks();
        return books.Where(b => b is not null)
            .Select(b => new BookDto(b!.Id, b.Title, b.PublishedYear, b.AuthorId));
    }

    public BookDto GetBookById(Guid id)
    {
        var book = _repository.Book.GetBookById(id);
        if (book is null)
            throw new BookNotFoundException(id);
        
        return new BookDto(book!.Id, book.Title, book.PublishedYear, book.AuthorId);
    }

    public BookDto AddBook(BookForCreationDto book)
    {
        var bookEntity = new Book
        {
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
        
        _repository.Book.CreateBook(bookEntity);
        var bookDto = new BookDto(bookEntity.Id, bookEntity.Title, bookEntity.PublishedYear, bookEntity.AuthorId);
        
        return bookDto;
    }

    public void UpdateBook(Guid id, BookForUpdateDto book)
    {
        var bookEntity = _repository.Book.GetBookById(id);
        
        if(bookEntity is null) 
            throw new BookNotFoundException(id);
        
        bookEntity.Title = book.Title;
        bookEntity.PublishedYear = book.PublishedYear;
        bookEntity.AuthorId = book.AuthorId;
    }

    public void DeleteBook(Guid id)
    {
        var book = _repository.Book.GetBookById(id);
        if(book is null)
            throw new BookNotFoundException(id);
        
        _repository.Book.DeleteBook(book);
    }
}