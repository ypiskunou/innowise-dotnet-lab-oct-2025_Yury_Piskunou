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
    public IActionResult GetAuthors()
    {
        var authors = _service.AuthorService.GetAllAuthors();
        return Ok(authors);
    }

    [HttpGet("{id:guid}", Name = "GetAuthorById")]
    public IActionResult GetAuthor(Guid id)
    {
        var author = _service.AuthorService.GetAuthorById(id);
        return Ok(author);
    }

    [HttpPost]
    public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
    {
        var createdAuthor = _service.AuthorService.AddAuthor(author);
        return CreatedAtRoute("GetAuthorById", new {id = createdAuthor.Id}, createdAuthor);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateAuthor(Guid id, [FromBody] AuthorForUpdateDto author)
    {
        _service.AuthorService.UpdateAuthor(id, author);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteAuthor(Guid id)
    {
        _service.AuthorService.DeleteAuthor(id);
        return NoContent();
    }
}