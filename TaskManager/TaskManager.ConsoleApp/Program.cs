using ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Application.Commands;
using TaskManager.Application.Services;
using TaskManager.Domain.Contracts;
using TaskManager.Repository;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDbConnectionFactory, PostgreSqlConnectionFactory>();
        services.AddScoped<ITaskRepository, PostgreSqlTaskRepository>();
        services.AddTransient<TaskApplicationService>();
        
        services.AddTransient<IAsyncCommand, ShowAllTasksCommand>();
        services.AddTransient<IAsyncCommand, AddTaskCommand>();
        services.AddTransient<IAsyncCommand, CompleteTaskCommand>();
        services.AddTransient<IAsyncCommand, DeleteTaskCommand>();  
        
        services.AddHostedService<ConsoleWorker>();
    })
    .Build();

await host.RunAsync();