using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace LibraryManager.Tests;

public class AuthorRepositoryTests
{
    private RepositoryContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new RepositoryContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact] 
    public async Task GetAuthorByIdAsync_ShouldReturnAuthor_WhenAuthorExists()
    {
        await using var context = CreateContext();
        var testAuthorId = Guid.NewGuid();
        context.Authors.Add(new Author { Id = testAuthorId, Name = "Test Author" });
        await context.SaveChangesAsync();

        var repository = new AuthorRepository(context);
        
        var author = await repository.GetAuthorByIdAsync(testAuthorId, trackChanges: false);
        
        Assert.NotNull(author);
        Assert.Equal(testAuthorId, author.Id);
    }
}