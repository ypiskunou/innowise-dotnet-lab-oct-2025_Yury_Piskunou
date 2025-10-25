using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TaskManager.Domain.Contracts;

namespace TaskManager.Repository;

public class PostgreSqlConnectionFactory: IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public PostgreSqlConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("PostgresConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}