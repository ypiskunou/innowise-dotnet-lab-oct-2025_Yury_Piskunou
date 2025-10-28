using LibraryManager.Presentation.ActionFfilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace LibraryManager.Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController: ControllerBase
{
    private readonly IServiceManager _service;

    public BooksController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _service.BookService.GetAllBooksAsync(false);
        return Ok(books);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    public async Task<IActionResult?> GetBook(Guid id)
    {
        var book = await _service.BookService.GetBookByIdAsync(id, false);
        return Ok(book);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto book)
    {
        var createdBook = await _service.BookService.AddBookAsync(book);
        return CreatedAtRoute("GetBookById", new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookForUpdateDto book)
    {
        await _service.BookService.UpdateBookAsync(id, book, true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        await _service.BookService.DeleteBookAsync(id, false);
        return NoContent();
    }
}