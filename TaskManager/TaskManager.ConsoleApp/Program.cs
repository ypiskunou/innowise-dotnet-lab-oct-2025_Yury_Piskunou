using ConsoleApp;
using ConsoleApp.DatabaseRegistrars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Application.Commands;
using TaskManager.Application.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var registrarTypes = typeof(Program).Assembly.GetTypes()
            .Where(t => typeof(IDatabaseRegistrar).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        
        var registrars = registrarTypes
            .Select(t => Activator.CreateInstance(t) as IDatabaseRegistrar)
            .Where(r => r is not null)
            .ToDictionary(r => r!.DatabaseName, r => r, StringComparer.OrdinalIgnoreCase);

        var dbType = context.Configuration["DatabaseType"] ?? "Postgres";

        if (registrars.TryGetValue(dbType, out var registrar))
        {
            registrar!.Register(services, context.Configuration);
        }
        else
        {
            throw new Exception($"Не найден регистратор для типа базы данных: '{dbType}'.");
        }
        
        services.AddTransient<TaskApplicationService>();
        
        services.AddTransient<IAsyncCommand, ShowAllTasksCommand>();
        services.AddTransient<IAsyncCommand, AddTaskCommand>();
        services.AddTransient<IAsyncCommand, CompleteTaskCommand>(); 
        services.AddTransient<IAsyncCommand, DeleteTaskCommand>(); 
        
        services.AddHostedService<ConsoleWorker>();
    })
    .Build();

await host.RunAsync();