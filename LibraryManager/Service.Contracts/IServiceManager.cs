namespace Service.Contracts;

public interface IServiceManager
{
    IAuthorService AuthorService { get; }
    IBookService BookService { get; }
}