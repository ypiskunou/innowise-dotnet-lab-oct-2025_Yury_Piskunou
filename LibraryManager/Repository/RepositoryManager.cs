using Entities;
using LibraryManager.Contracts;

namespace Repository;

public class RepositoryManager: IRepositoryManager
{
    private readonly Lazy<IAuthorRepository> _author;
    private readonly Lazy<IBookRepository> _book;

    public RepositoryManager()
    {
        _author = new Lazy<IAuthorRepository>(() => new AuthorRepository());
        _book = new Lazy<IBookRepository>(() => new BookRepository());
    }
    
    public IAuthorRepository Author => _author.Value;
    public IBookRepository Book => _book.Value;
}