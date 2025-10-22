using System.Data;
using Npgsql;
using TaskManager.Domain.Contracts;

namespace TaskManager.Repository;

public class PostgreSqlConnectionFactory: IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public PostgreSqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}