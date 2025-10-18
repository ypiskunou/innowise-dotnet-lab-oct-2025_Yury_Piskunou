using TaskManager.Domain.Contracts;

namespace TaskManager.Tests;

public interface IDatabaseProvider
{
    string DatabaseName { get; }
    
    string CleanupSql { get; }
    
    IDbConnectionFactory CreateDbFactory(string connectionString);
}