using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Features.Feeds;
using TheAggregate.Api.Features.SyndicationFeeds;
using TheAggregate.Api.Jobs;
using TheAggregate.Api.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// configure json serialization
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
    options.JsonSerializerOptions.WriteIndented = false;
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddScoped<IFeedsRepository, FeedsRepository>();
builder.Services.AddScoped<IFeedsService, FeedsService>();
builder.Services.AddScoped<IAggregationService, AggregationService>();
builder.Services.AddHostedService<PipelineInitializerHostedService>();

string connectionString;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    connectionString = builder.Configuration.GetConnectionString("DevConnection")??
                       throw new Exception("Development connection string not found");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??
                       throw new InvalidOperationException("Default connection string not found");
}

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(connectionString)));

builder.Services.AddHangfireServer(options => { options.SchedulePollingInterval = TimeSpan.FromSeconds(15); });

builder.Services.AddScoped<AggregationPipelineJob>();

var app = builder.Build();

await Seeder.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();