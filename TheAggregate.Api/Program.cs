using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Features.Feeds;
using TheAggregate.Api.Features.Identity;
using TheAggregate.Api.Features.SyndicationFeeds;
using TheAggregate.Api.Jobs;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// configure json serialization
builder.Services.Configure<JsonOptions>(options =>
{
    // maps PascalCase .NET property keys to camelCase json keys
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    // protect from infinite loops in json parent->child relationships
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
    // turn off json indentation to save space
    options.JsonSerializerOptions.WriteIndented = false;
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IJwtService, JwtService>();
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

// auth endpoints, customized during middleware
// configuration later in this file
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserResolverService, UserResolverService>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var OnlyAllowLocalhostOrigins = "_onlyAllowLocalhostOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OnlyAllowLocalhostOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost");
        });
});

var app = builder.Build();

await Seeder.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.UseSwaggerUI(
        options =>
        {
            // http://localhost:5050/swagger/index.html
            options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
            // options.RoutePrefix = "openapi";
        });
    app.MapScalarApiReference(options =>
    {
        // http://localhost:5050/scalar/v1
        options.WithTitle("My API");
        options.WithTheme(ScalarTheme.DeepSpace);
        options.WithSidebar(true);
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.CustomMapIdentityApi<ApplicationUser>()
    .RequireCors(OnlyAllowLocalhostOrigins);
app.UseHangfireDashboard();

app.MapControllers().WithOpenApi();
app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager,
        [FromBody] object empty) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .WithOpenApi()
    .RequireAuthorization()
    .RequireCors(OnlyAllowLocalhostOrigins);

app.Run();