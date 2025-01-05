using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext() {}
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionItem> CollectionItems { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<FeedItem> FeedItems { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<UserState> UserStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // properties for enabling full-text search
        modelBuilder.Entity<FeedItem>()
            .HasGeneratedTsVectorColumn(
                fi => fi.SearchVector!,
                "english",
                fi => new { fi.Title, fi.Summary })
            .HasIndex(fi => fi.SearchVector)
            .HasMethod("GIN");

        modelBuilder.Entity<Feed>()
            .HasGeneratedTsVectorColumn(
                fi => fi.SearchVector,
                "english",
                fi => new { fi.Title, fi.Description })
            .HasIndex(fi => fi.SearchVector)
            .HasMethod("GIN");
    }
}