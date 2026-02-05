using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MM.Users;

public static class UsersModuleExtensions
{
  public static IServiceCollection AddUsersModuleServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
    {
      options.UseSqlServer(connectionString);
    });
    
    services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();
    
    logger.Information("Users module services added");
    
    return services;
  }
}
