namespace LibraryManager.Contracts;

public interface IRepositoryManager
{
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
    void Save();
}