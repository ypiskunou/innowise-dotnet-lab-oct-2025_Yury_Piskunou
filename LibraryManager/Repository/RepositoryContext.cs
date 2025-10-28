using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext: DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }
    
    DbSet<Author>? Authors { get; set; }
    DbSet<Book>? Books { get; set; }
}