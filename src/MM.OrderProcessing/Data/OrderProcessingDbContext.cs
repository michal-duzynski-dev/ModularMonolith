using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MM.OrderProcessing.Data;

internal class OrderProcessingDbContext : IdentityDbContext
{
  public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options) : base(options)
  {
    
  }
  
  public DbSet<Order> Orders { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.HasDefaultSchema("orderProcessing");
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
  }
}

