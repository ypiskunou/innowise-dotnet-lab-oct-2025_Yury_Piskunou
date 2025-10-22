using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Contracts;
using TaskManager.Domain.Entities;
using TaskManager.Repository;

namespace ConsoleApp.DatabaseRegistrars;

public class MsSqlRegistrar : IDatabaseRegistrar
{
    public string DatabaseName => "MsSql";
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory>(sp =>
            new MsSqlConnectionFactory(configuration.GetConnectionString("MsSqlConnection")));
        services.AddSingleton<ITaskRepository, MsSqlTaskRepository>();
        Console.WriteLine("Используется база данных MS SQL Server.");
    }
}