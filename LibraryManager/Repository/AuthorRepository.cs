using System.Linq.Expressions;
using Entities;
using LibraryManager.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Author?>> GetAllAuthorsAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(a => a.Name)
        .ToListAsync();

    public async Task<IEnumerable<Author?>> SearchAuthorsByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(a => a.Name.ToLower().Contains(name.ToLower()), trackChanges)
            .OrderBy(a => a.Name)
            .ToListAsync();

    public IQueryable<Author?> GetAllAuthorsWithBooksQueryable(bool trackChanges) => 
        FindAll(trackChanges)
            .OrderBy(a => a.Name)
            .Include(a => a.Books);

    public async Task<IEnumerable<T>> GetAuthorsAsAsync<T>(IQueryable<Author?> authors,
        Expression<Func<Author, T>> selector)
    {
        return await authors.Select(selector).ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(a => a.Id == id, trackChanges)
            .FirstOrDefaultAsync();

    public void CreateAuthor(Author author) => Create(author);

    public void DeleteAuthor(Author author) => Delete(author);
}