using LibraryManager.Presentation.ActionFfilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace LibraryManager.Presentation.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController: ControllerBase
{
    private readonly IServiceManager _service;

    public AuthorsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _service.AuthorService.GetAllAuthorsAsync(false);
        return Ok(authors);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAuthorsByName([FromQuery] string name)
    {
        var authors = await _service.AuthorService.SearchAuthorsByNameAsync(name, false);
        return Ok(authors);
    }

    [HttpGet("with-book-counts")]
    public async Task<IActionResult> GetAuthorsWithBookCounts()
    {
        var authorsWithBookCounts = 
            await _service.AuthorService.GetAuthorsWithBookCountsAsync(false);
        
        return Ok(authorsWithBookCounts);
    }

    [HttpGet("{id:guid}", Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        var author = await _service.AuthorService.GetAuthorByIdAsync(id, false);
        return Ok(author);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorForCreationDto author)
    {
        var createdAuthor = await _service.AuthorService.AddAuthorAsync(author);
        return CreatedAtRoute("GetAuthorById", new {id = createdAuthor.Id}, createdAuthor);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorForUpdateDto author)
    {
        await _service.AuthorService.UpdateAuthorAsync(id, author, true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        await _service.AuthorService.DeleteAuthorAsync(id, false);
        return NoContent();
    }
}