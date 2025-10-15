using Dapper;
using TaskManager.Domain.Contracts;
using TaskManager.Domain.Entities;

namespace TaskManager.Repository;

public class PostgreSqlTaskRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public PostgreSqlTaskRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task AddAsync(TaskItem task)
    {
        var connection = _dbConnectionFactory.CreateConnection();

        var sql = @"INSERT INTO ""Tasks"" (""Title"", ""Description"", ""IsCompleted"") 
            VALUES (@Title, @Description, @IsCompleted);";

        await connection.ExecuteAsync(sql, task);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        return await connection.QueryAsync<TaskItem>(@"SELECT * FROM ""Tasks""");
    }

    public async Task<bool> UpdateStatusAsync(int id, bool isCompleted)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        var sql = @"UPDATE ""Tasks"" SET ""IsCompleted"" = @IsCompleted WHERE ""Id"" = @Id";
        
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id, IsCompleted = isCompleted });

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        var sql = @"DELETE FROM ""Tasks"" WHERE ""Id"" = @Id";
        
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        
        return affectedRows > 0;
    }
}