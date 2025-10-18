using TaskManager.Domain.Contracts;
using TaskManager.Repository;

namespace TaskManager.Tests;

public class MsSqlProvider : IDatabaseProvider
{
    public string DatabaseName => "MsSql";
    public string CleanupSql => @"DELETE FROM [Tasks]; DBCC CHECKIDENT ('[Tasks]', RESEED, 0);";
    
    public IDbConnectionFactory CreateDbFactory(string connectionString)
    {
        return new MsSqlConnectionFactory(connectionString);
    }
}