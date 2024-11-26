using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() {}
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionItem> CollectionItems { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<FeedItem> FeedItems { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<JobRecord> Jobs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}