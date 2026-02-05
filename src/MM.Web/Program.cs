using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MM.Books;
using MM.Users;
using Serilog;


var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, configuration) => 
  configuration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
  .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Auth:JwtSecret"])
  .AddAuthorization()
  .SwaggerDocument();

builder.Services.AddBookServices(builder.Configuration, logger);
builder.Services.AddUsersModuleServices(builder.Configuration, logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints()
  .UseSwaggerGen();

app.Run();

