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
    public IActionResult GetBooks()
    {
        var books = _service.BookService.GetAllBooks();
        return Ok(books);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    public IActionResult GetBook(Guid id)
    {
        var book = _service.BookService.GetBookById(id);
        return Ok(book);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public IActionResult CreateBook([FromBody] BookForCreationDto book)
    {
        var createdBook = _service.BookService.AddBook(book);
        return CreatedAtRoute("GetBookById", new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public IActionResult UpdateBook(Guid id, [FromBody] BookForUpdateDto book)
    {
        _service.BookService.UpdateBook(id, book);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBook(Guid id)
    {
        _service.BookService.DeleteBook(id);
        return NoContent();
    }
}