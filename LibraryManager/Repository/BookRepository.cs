using Entities;
using LibraryManager.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class BookRepository: RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Book?>> GetAllBooksAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(b => b.Title)
        .ToListAsync();

    public async Task<Book?> GetBookByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(b => b.Id == id, trackChanges).FirstOrDefaultAsync();

    public void CreateBook(Book book) => Create(book);

    public void DeleteBook(Book book) => Delete(book);
}