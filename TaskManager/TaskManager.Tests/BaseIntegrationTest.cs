using Dapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Domain.Contracts;

namespace TaskManager.Tests;

public abstract class BaseIntegrationTest : IDisposable
{
    protected readonly IDbConnectionFactory DbFactory;
    
    private static readonly IReadOnlyDictionary<string, IDatabaseProvider> _providers;

    static BaseIntegrationTest()
    {
        _providers = typeof(BaseIntegrationTest).Assembly.GetTypes()
            .Where(t => typeof(IDatabaseProvider).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as IDatabaseProvider)
            .ToDictionary(p => p.DatabaseName, p => p, StringComparer.OrdinalIgnoreCase);
    }

    protected BaseIntegrationTest()
    {
        var childClassName = GetType().Name;
        
        var provider = _providers.Values.FirstOrDefault(p =>
            childClassName.StartsWith(p.DatabaseName, StringComparison.OrdinalIgnoreCase));

        if (provider is null)
        {
            throw new Exception($"Не удалось найти провайдер для тестового класса '{childClassName}'.");
        }

        var dbTypeName = provider.DatabaseName;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Tests.json")
            .Build();

        var connectionStringName = dbTypeName + "Connection";
        var connectionString = configuration.GetConnectionString(connectionStringName);
        
        DbFactory = provider.CreateDbFactory(connectionString);
        
        using var connection = DbFactory.CreateConnection();
        connection.Execute(provider.CleanupSql);
    }

    public void Dispose()
    {
    }
}