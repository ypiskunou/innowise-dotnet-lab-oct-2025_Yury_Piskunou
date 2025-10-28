using AutoMapper;
using Entities;
using Entities.Exceptions;
using LibraryManager.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class BookService: IBookService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public BookService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
    {
        var books = await _repository.Book.GetAllBooksAsync(trackChanges);
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        
        return booksDto;
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id, bool trackChanges)
    {
        var book = await GetBookAndCheckIfExistsAsync(id, trackChanges);
        
        return _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> AddBookAsync(BookForCreationDto book)
    {
        var bookEntity = _mapper.Map<Book>(book);
        _repository.Book.CreateBook(bookEntity);
        await _repository.SaveAsync();
        
        return _mapper.Map<BookDto>(bookEntity);
    }

    public async Task UpdateBookAsync(Guid id, BookForUpdateDto book, bool trackChanges)
    {
        var bookEntity = await GetBookAndCheckIfExistsAsync(id, trackChanges);
        _mapper.Map(book, bookEntity);
        await _repository.SaveAsync();
    }

    public async Task DeleteBookAsync(Guid id, bool trackChanges)
    {
        var bookEntity = await GetBookAndCheckIfExistsAsync(id, trackChanges);
        _repository.Book.DeleteBook(bookEntity);
        await _repository.SaveAsync();
    }

    private async Task<Book> GetBookAndCheckIfExistsAsync(Guid id, bool trackChanges)
    {
        var book = await _repository.Book.GetBookByIdAsync(id, trackChanges);
        if(book is null)
            throw new BookNotFoundException(id);
        
        return book;
    }
}