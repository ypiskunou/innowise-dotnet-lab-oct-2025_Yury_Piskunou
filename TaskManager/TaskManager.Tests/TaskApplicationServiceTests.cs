using FluentAssertions;
using Moq;
using TaskManager.Application.Services;
using TaskManager.Domain.Contracts;
using TaskManager.Domain.Entities;

namespace TaskManager.Tests;

public class TaskApplicationServiceTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly TaskApplicationService _service;
    
    public TaskApplicationServiceTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _service = new TaskApplicationService(_mockTaskRepository.Object);
    }

    #region AddNewTaskAsync Tests

    [Fact]
    public async Task AddNewTaskAsync_WithValidTitle_ShouldCallRepositoryAddAsync()
    {
        var title = "Валидный заголовок";
        var description = "Описание";
        
        await _service.AddNewTaskAsync(title, description);
        
        _mockTaskRepository.Verify(repo => repo.AddAsync(It.IsAny<TaskItem>()), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task AddNewTaskAsync_WithInvalidTitle_ShouldThrowArgumentException(string invalidTitle)
    {
        Func<Task> action = () => _service.AddNewTaskAsync(invalidTitle, "Описание");
        
        await action.Should().ThrowAsync<ArgumentException>();
        
        _mockTaskRepository.Verify(repo => repo.AddAsync(It.IsAny<TaskItem>()), Times.Never);
    }

    #endregion

    #region GetAllTasksAsync Tests

    [Fact]
    public async Task GetAllTasksAsync_WhenRepositoryReturnsTasks_ShouldReturnAllTasks()
    {
        var fakeTasks = new List<TaskItem>
        {
            new(1, "Задача 1", null, false, DateTime.UtcNow),
            new(2, "Задача 2", null, true, DateTime.UtcNow)
        };
        
        _mockTaskRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeTasks);
        
        var result = (await _service.GetAllTasksAsync()).ToList();
        
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        
        result.Should().BeEquivalentTo(fakeTasks);
    }

    #endregion
    
    #region DeleteTaskAsync Tests

    [Fact]
    public async Task DeleteTaskAsync_WhenRepositorySucceeds_ShouldReturnTrue()
    {
        var taskId = 1;
        
        _mockTaskRepository.Setup(repo => repo.DeleteAsync(taskId)).ReturnsAsync(true);
        
        var result = await _service.DeleteTaskAsync(taskId);
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task DeleteTaskAsync_WhenRepositoryFails_ShouldReturnFalse()
    {
        var taskId = 99; 
        
        _mockTaskRepository.Setup(repo => repo.DeleteAsync(taskId)).ReturnsAsync(false);
        
        var result = await _service.DeleteTaskAsync(taskId);
        
        result.Should().BeFalse();
    }
    
    #endregion
    
    #region CompleteTaskAsync Tests
    
    [Fact]
    public async Task CompleteTaskAsync_WhenTaskExists_ShouldCallUpdateWithTrue()
    {
        var taskId = 1;
        _mockTaskRepository.Setup(repo => repo.UpdateStatusAsync(taskId, true)).ReturnsAsync(true);
        
        var result = await _service.CompleteTaskAsync(taskId);
        
        result.Should().BeTrue();
        
        _mockTaskRepository.Verify(repo => repo.UpdateStatusAsync(taskId, true), Times.Once);
    }
    
    #endregion
}