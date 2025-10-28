using Entities;
using LibraryManager.Contracts;

namespace Repository;

public class RepositoryManager: IRepositoryManager
{
    private readonly RepositoryContext _context;
    
    private readonly Lazy<IAuthorRepository> _author;
    private readonly Lazy<IBookRepository> _book;

    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _author = new Lazy<IAuthorRepository>(() => new AuthorRepository(context));
        _book = new Lazy<IBookRepository>(() => new BookRepository(context));
    }
    
    public IAuthorRepository Author => _author.Value;
    public IBookRepository Book => _book.Value;
    
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}