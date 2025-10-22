using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Contracts;
using TaskManager.Repository;

namespace ConsoleApp.DatabaseRegistrars;

public class PostgreSqlRegistrar: IDatabaseRegistrar
{
    public string DatabaseName => "Postgres";
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory>(sp => 
            new PostgreSqlConnectionFactory(configuration.GetConnectionString("PostgresConnection")));
        services.AddSingleton<ITaskRepository, PostgreSqlTaskRepository>();
        Console.WriteLine("Используется база данных PostgreSQL.");
    }
}
