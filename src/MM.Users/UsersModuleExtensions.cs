using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MM.Users.Data;
using MM.Users.UseCases;
using Serilog;

namespace MM.Users;

public static class UsersModuleExtensions
{
  public static IServiceCollection AddUsersModuleServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
    {
      options.UseSqlServer(connectionString);
    });
    services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();
    
    services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();
    mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);
    logger.Information("Users module services added");
    
    return services;
  }
}
