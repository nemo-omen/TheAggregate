using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Models;
using TheAggregate.Shared;
using TheAggregate.Shared.SeedData;

namespace TheAggregate.Api.Data;

public static class Seeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var feedsSeedPath = SeedDataHelper.GetSeedDataPath("Feeds.json");
        var seedSettings = new SeedDataSettings
        {
            FeedsSeed = feedsSeedPath,
        };
        
        var feedsSeed = SeedDataHelper.LoadSeedData<Feed>(seedSettings.FeedsSeed); 
        var existingFeedRecords = await context.Feeds.ToListAsync();

        foreach (var fSeed in feedsSeed)
        {
            var existingFeed = existingFeedRecords.FirstOrDefault(f => f.WebUrl == fSeed.WebUrl);
            if (existingFeed is null)
            {
                var feedWithId = new Feed
                {
                    Id = Guid.NewGuid(),
                    Title = fSeed.Title,
                    Description = fSeed.Description,
                    WebUrl = fSeed.WebUrl,
                    FeedUrl = fSeed.FeedUrl,
                    ImageUrl = fSeed.ImageUrl,
                    Language = fSeed.Language,
                    Categories = fSeed.Categories,
                };
        
                await context.AddAsync(feedWithId);
            }
            else
            {
                existingFeed.Title = fSeed.Title;
                existingFeed.Description = fSeed.Description;
                existingFeed.WebUrl = fSeed.WebUrl;
                existingFeed.FeedUrl = fSeed.FeedUrl;
                existingFeed.ImageUrl = fSeed.ImageUrl;
                existingFeed.Language = fSeed.Language;
                existingFeed.Categories = fSeed.Categories;
            }
        }
        await context.SaveChangesAsync();
    }
}