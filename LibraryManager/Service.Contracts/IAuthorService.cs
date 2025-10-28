using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAuthorService
{
    IEnumerable<AuthorDto> GetAllAuthors();
    AuthorDto GetAuthorById(Guid id);
    AuthorDto AddAuthor(AuthorForCreationDto author);
    void UpdateAuthor(Guid id, AuthorForUpdateDto author);
    void DeleteAuthor(Guid id);
}