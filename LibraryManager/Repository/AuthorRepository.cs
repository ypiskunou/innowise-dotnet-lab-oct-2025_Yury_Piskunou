using Entities;
using LibraryManager.Contracts;

namespace Repository;

public class AuthorRepository: IAuthorRepository
{
    public IEnumerable<Author?> GetAllAuthors() => InMemoryDataStorage.Authors.ToList();

    public Author? GetAuthorById(Guid id) => InMemoryDataStorage.Authors
        .FirstOrDefault(a => a != null && a.Id == id);

    public void CreateAuthor(Author author)
    {
        author.Id = Guid.NewGuid();
        InMemoryDataStorage.Authors.Add(author);
    }

    public void DeleteAuthor(Author author) => InMemoryDataStorage.Authors.Remove(author);
}