using LibraryManager.Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager: IServiceManager
{
    private Lazy<IAuthorService> _authorService;
    private Lazy<IBookService> _bookService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _authorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager));
        _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager));
    }
    
    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
}