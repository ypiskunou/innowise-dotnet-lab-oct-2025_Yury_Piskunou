namespace LibraryManager.Contracts;

public interface IRepositoryManager
{
    IAuthorRepository Author { get; }
    IBookRepository Book { get; }
    
    Task SaveAsync();
}