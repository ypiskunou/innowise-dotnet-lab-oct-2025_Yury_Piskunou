using TaskManager.Domain.Entities;
using TaskManager.Repository;
using FluentAssertions;

namespace TaskManager.Tests;

public class MsSqlTaskRepositoryTests : BaseIntegrationTest
{
    [Fact]
    public async Task AddAsync_ShouldInsertSingleTask()
    {
        var repository = new MsSqlTaskRepository(DbFactory);
        var newTask = new TaskItem(0, "Тест добавления для MS SQL", 
            "Описание", false, DateTime.MinValue);
        
        await repository.AddAsync(newTask);
        
        var tasks = (await repository.GetAllAsync()).ToList();

        tasks.Should().HaveCount(1);
        
        var addedTask = tasks.Single();
        addedTask.Title.Should().Be("Тест добавления для MS SQL");
        addedTask.Description.Should().Be("Описание");
        addedTask.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_WhenTaskExists_ShouldRemoveIt()
    {
        var repository = new MsSqlTaskRepository(DbFactory);
        await repository.AddAsync(new TaskItem(0, 
            "Задача на удаление", null, false, DateTime.MinValue));
        
        var taskToDelete = (await repository.GetAllAsync()).Single();
        var idToDelete = taskToDelete.Id;
        
        var result = await repository.DeleteAsync(idToDelete);
        var tasksAfterDelete = await repository.GetAllAsync();
        
        result.Should().BeTrue();
        tasksAfterDelete.Should().BeEmpty();
    }
    
    [Fact]
    public async Task UpdateStatusAsync_WhenTaskExists_ShouldChangeStatus()
    {
        var repository = new MsSqlTaskRepository(DbFactory);
        await repository.AddAsync(new TaskItem(0, 
            "Задача для обновления", null, false, DateTime.MinValue));
        
        var taskToUpdate = (await repository.GetAllAsync()).Single();
        var idToUpdate = taskToUpdate.Id;
        taskToUpdate.IsCompleted.Should().BeFalse();
        
        var result = await repository.UpdateStatusAsync(idToUpdate, true);
        
        result.Should().BeTrue();

        var taskAfterUpdate = (await repository.GetAllAsync()).Single();
        taskAfterUpdate.IsCompleted.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetAllAsync_WhenDatabaseIsEmpty_ShouldReturnEmptyList()
    {
        var repository = new PostgreSqlTaskRepository(DbFactory);
        
        var tasks = (await repository.GetAllAsync()).ToList();
        
        tasks.Should().NotBeNull();
        tasks.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_WhenTasksExist_ShouldReturnAllTasks()
    {
        var repository = new PostgreSqlTaskRepository(DbFactory);
        
        await repository.AddAsync(new TaskItem(0, 
            "Задача 1", "Описание 1", false, DateTime.MinValue));
        await repository.AddAsync(new TaskItem(0, 
            "Задача 2", "Описание 2", true, DateTime.MinValue));
        await repository.AddAsync(new TaskItem(0, 
            "Задача 3", null, false, DateTime.MinValue));
        
        var tasks = (await repository.GetAllAsync()).ToList();
        
        tasks.Should().HaveCount(3);
        
        tasks.Should().Contain(t => t.Title == "Задача 1" && t.IsCompleted == false);
        tasks.Should().Contain(t => t.Title == "Задача 2" && t.IsCompleted == true);
        tasks.Should().Contain(t => t.Title == "Задача 3" && t.Description == null);
    }
}