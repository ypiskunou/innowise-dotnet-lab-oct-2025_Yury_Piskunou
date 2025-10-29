using System.Linq.Expressions;
using AutoMapper;
using Entities;
using Entities.Exceptions;
using LibraryManager.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class AuthorService : IAuthorService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public AuthorService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync(bool trackChanges)
    {
        var authors = await _repository.Author.GetAllAuthorsAsync(trackChanges);
        var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

        return authorsDto;
    }

    public async Task<IEnumerable<AuthorDto>> SearchAuthorsByNameAsync(string name, bool trackChanges)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Enumerable.Empty<AuthorDto>();

        var authors = await _repository.Author.SearchAuthorsByNameAsync(name, trackChanges);

        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public async Task<IEnumerable<AuthorWithBookCountDto>> GetAuthorsWithBookCountsAsync(bool trackChanges)
    {
        var authorsQuery = _repository.Author.GetAuthorsWithBooks(false);

        Expression<Func<Author, AuthorWithBookCountDto>> selector = author =>
            new AuthorWithBookCountDto(
                author.Id,
                author.Name,
                author.Books.Count()
            );

        var authors = await _repository.Author.GetAuthorsAsAsync(authorsQuery, selector);

        return authors;
    }

    public async Task<AuthorDto> GetAuthorByIdAsync(Guid id, bool trackChanges)
    {
        var author = await GetAuthorIfExistsAsync(id, trackChanges);
        var authorDto = _mapper.Map<AuthorDto>(author);

        return authorDto;
    }

    public async Task<AuthorDto> AddAuthorAsync(AuthorForCreationDto author)
    {
        var authorEntity = _mapper.Map<Author>(author);
        _repository.Author.CreateAuthor(authorEntity);
        await _repository.SaveAsync();

        var authorDto = _mapper.Map<AuthorDto>(authorEntity);

        return authorDto;
    }

    public async Task UpdateAuthorAsync(Guid id, AuthorForUpdateDto author, bool trackChanges)
    {
        var authorEntity = await GetAuthorIfExistsAsync(id, trackChanges);
        _mapper.Map(author, authorEntity);
        await _repository.SaveAsync();
    }

    public async Task DeleteAuthorAsync(Guid id, bool trackChanges)
    {
        var author = await GetAuthorIfExistsAsync(id, trackChanges);
        _repository.Author.DeleteAuthor(author);
        await _repository.SaveAsync();
    }

    private async Task<Author> GetAuthorIfExistsAsync(Guid id, bool trackChanges)
    {
        var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges);
        if (author is null)
            throw new AuthorNotFoundException(id);

        return author;
    }
}