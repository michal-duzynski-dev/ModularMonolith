using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;

namespace MM.Books;

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services, 
      ConfigurationManager configuration, ILogger logger, List<Assembly> mediatRAssemblies)
    {
      string? connectionString = configuration.GetConnectionString("BooksConnectionString");
      services.AddDbContext<BookDbContext>(options =>
      {
        options.UseSqlServer(connectionString);
      });
      
      mediatRAssemblies.Add(typeof(BookServiceExtensions).Assembly);
      services.AddScoped<IBookRepository, EfBookRepository>();
      services.AddScoped<IBookService, BookService>();
        
      logger.Information("Books module services added");
        
      return services;
    }
}
