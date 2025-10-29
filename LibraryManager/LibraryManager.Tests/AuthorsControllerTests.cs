using LibraryManager.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace LibraryManager.Tests;

public class AuthorsControllerTests
{
    [Fact]
    public async Task GetAuthor_ShouldReturnOk_WhenAuthorExists()
    {
        var serviceManagerMock = new Mock<IServiceManager>();
        var testAuthorDto = new AuthorDto(Guid.NewGuid(), "Test", DateTime.Now);
        
        serviceManagerMock.Setup(s => s.AuthorService.GetAuthorByIdAsync(It.IsAny<Guid>(), 
                It.IsAny<bool>()))
            .ReturnsAsync(testAuthorDto);

        var controller = new AuthorsController(serviceManagerMock.Object);
        
        var result = await controller.GetAuthor(Guid.NewGuid());
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        
        var returnedDto = Assert.IsType<AuthorDto>(okResult.Value);
        Assert.Equal(testAuthorDto.Id, returnedDto.Id);
    }
}