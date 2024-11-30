using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Features.FeedAggregation;
using TheAggregate.Api.Features.Feeds;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared;
using TheAggregate.Api.Shared.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddFastEndpoints()
    .AddJobQueues<JobRecord, JobStorageProvider>();

builder.Services.AddScoped<IFeedsRepository, FeedsRepository>();
builder.Services.AddScoped<IFeedsService, FeedsService>();
builder.Services.AddScoped<IAggregationService, AggregationService>();


var env = builder.Environment;
string connectionString;
if (env.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DevConnection")
                       ?? throw new Exception("DevConnection is not set in appsettings.json");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new Exception("DefaultConnection is not set in appsettings.json");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
    
builder.Services.AddHostedService<PipelineInitializerHostedService>();

var app = builder.Build();

await Seeder.SeedAsync(app.Services);

app.UseDefaultExceptionHandler()
    .UseFastEndpoints(c =>
    {
        c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        c.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .UseJobQueues();

app.Run();