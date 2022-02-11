using Microsoft.OpenApi.Models;

using MinimalApi.Endpoint.Configurations.Extensions;
using MinimalApi.Endpoint.Extensions;

using Serilog;

using TestJob01.API;
using TestJob01.API.Configuration;
using TestJob01.API.Middleware;
using TestJob01.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfigurationFile();

// remove default logging providers
builder.Logging.ClearProviders();
// Serilog configuration		
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
// Register Serilog
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpoints();

TestJob01.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddCoreServices(builder.Configuration);

const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options => {
    options.AddPolicy(name: CORS_POLICY,
                      builder => {
                          builder.AllowAnyOrigin();
                          //builder.AllowOrigins();
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test job 01 API", Version = "v1" });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test job 01 API V1");
    });
}

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope()) {
    var scopedProvider = scope.ServiceProvider;
    try {
        var taskJob01Context = scopedProvider.GetRequiredService<TestJob01Context>();
        await TestJob01ContextSeed.SeedAsync(taskJob01Context, app.Logger);
    } catch (Exception ex) {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(CORS_POLICY);

app.MapEndpoints();

app.Run();
public partial class Program { }