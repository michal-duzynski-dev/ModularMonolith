using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MM.OrderProcessing.Data;
using Serilog;

namespace MM.OrderProcessing;

public static class OrderProcessingModuleExtensions
{
  public static IServiceCollection AddOrderProcessingModuleServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("OrderProcessingConnectionString");
    services.AddDbContext<OrderProcessingDbContext>(options =>
    {
      options.UseSqlServer(connectionString);
    });
    services.AddScoped<IOrderRepository, EfOrderRepository>();
    
    mediatRAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);
    logger.Information("Order Processing module services added");
    
    return services;
  }
}
