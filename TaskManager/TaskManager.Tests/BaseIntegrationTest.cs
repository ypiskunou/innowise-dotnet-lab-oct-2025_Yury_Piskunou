using Dapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Domain.Contracts;
using TaskManager.Repository;

namespace TaskManager.Tests;

public abstract class BaseIntegrationTest : IDisposable
{
    protected readonly IDbConnectionFactory DbFactory;

    protected BaseIntegrationTest()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Tests.json")
            .Build();

        DbFactory = new PostgreSqlConnectionFactory(configuration);
        
        CleanupDatabase();
    }
    
    private void CleanupDatabase()
    {
        using var connection = DbFactory.CreateConnection();
        connection.Execute(@"TRUNCATE TABLE ""Tasks"" RESTART IDENTITY CASCADE");
    }

    public void Dispose()
    {
    }
}