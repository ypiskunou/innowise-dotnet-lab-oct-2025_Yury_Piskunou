using TaskManager.Domain.Contracts;
using TaskManager.Repository;

namespace TaskManager.Tests;

public class PostgreSqlProvider : IDatabaseProvider
{
    public string DatabaseName => "Postgres";
    public string CleanupSql => @"TRUNCATE TABLE ""Tasks"" RESTART IDENTITY CASCADE";

    public IDbConnectionFactory CreateDbFactory(string connectionString)
    {
        return new PostgreSqlConnectionFactory(connectionString);
    }
}