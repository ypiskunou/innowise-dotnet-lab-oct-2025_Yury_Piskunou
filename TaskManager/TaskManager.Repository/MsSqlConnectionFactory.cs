using System.Data;
using Microsoft.Data.SqlClient;
using TaskManager.Domain.Contracts;

namespace TaskManager.Repository;

public class MsSqlConnectionFactory: IDbConnectionFactory
{
    private readonly string _connectionString;

    public MsSqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}