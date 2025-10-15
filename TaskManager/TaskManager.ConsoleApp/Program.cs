using ConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Application.Services;
using TaskManager.Domain.Contracts;
using TaskManager.Repository;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var dbType = context.Configuration["DatabaseType"] ?? "Postgres";
        string connectionString;

        if (dbType.Equals("Postgres", StringComparison.OrdinalIgnoreCase))
        {
            connectionString = context.Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddSingleton<IDbConnectionFactory>(sp => new PostgreSqlConnectionFactory(connectionString));
            services.AddSingleton<ITaskRepository, PostgreSqlTaskRepository>();
        }
        else if (dbType.Equals("MsSql", StringComparison.OrdinalIgnoreCase))
        {
            connectionString = context.Configuration.GetConnectionString("MsSqlConnection");
            services.AddSingleton<IDbConnectionFactory>(sp => new MsSqlConnectionFactory(connectionString));
            services.AddSingleton<ITaskRepository, MsSqlTaskRepository>();
        }
        else
        {
            throw new Exception($"Неподдерживаемый тип базы данных: {dbType}");
        }

        services.AddTransient<TaskApplicationService>();
        
        services.AddHostedService<ConsoleWorker>();
    })
    .Build();

await host.RunAsync();