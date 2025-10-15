using System.Data;

namespace TaskManager.Domain.Contracts;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}