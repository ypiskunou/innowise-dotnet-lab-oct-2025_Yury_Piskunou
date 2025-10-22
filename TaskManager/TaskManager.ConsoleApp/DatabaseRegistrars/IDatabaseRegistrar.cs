using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.DatabaseRegistrars;

public interface IDatabaseRegistrar
{
    string DatabaseName { get; } 
    void Register(IServiceCollection services, IConfiguration configuration);
}