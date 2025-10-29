using AutoMapper;
using Entities;
using Entities.Exceptions;
using LibraryManager.Contracts;
using Moq;
using Service;

namespace LibraryManager.Tests;

public class AuthorServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthorService _authorService;

    public AuthorServiceTests()
    {
        _repositoryMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        
        _authorService = new AuthorService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAuthorByIdAsync_ShouldThrowNotFoundException_WhenAuthorDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        
        _repositoryMock.Setup(r => r.Author.GetAuthorByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
            .ReturnsAsync((Author?)null);

        
        await Assert.ThrowsAsync<AuthorNotFoundException>(() => 
            _authorService.GetAuthorByIdAsync(nonExistentId, trackChanges: false)
        );
    }
}