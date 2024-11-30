using FluentResults;
using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public interface IFeedsRepository
{
    Task<Result<List<Feed>>> GetFeedsAsync();
    Task<Result<Feed>> GetFeedByIdAsync(int id);
    Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl);
    Task<Result<List<Feed>>> UpdateFeedsAsync(List<Feed> feeds);
    // Task<Result<Feed>> CreateFeedAsync(Feed feed);
    Task<Result<Feed>> UpdateFeedAsync(Feed feed);
    // Task<Result<Feed>> DeleteFeedAsync(int id);
}

public class FeedsRepository : IFeedsRepository
{
    private readonly ApplicationDbContext _context;
    
    public FeedsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Feed>>> GetFeedsAsync()
    {
        var feeds = await _context.Feeds
            .AsNoTracking()
            .Include(f => f.Items)
            .OrderBy(f => f.Title)
            .ToListAsync();
        
        return Result.Ok(feeds);
    }

    public Task<Result<Feed>> GetFeedByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Result<List<Feed>>> UpdateFeedsAsync(List<Feed> feeds)
    {
        _context.Feeds.UpdateRange(feeds);
        await _context.SaveChangesAsync();
        
        return Result.Ok(feeds);
    }

    public async Task<Result<Feed>> UpdateFeedAsync(Feed feed)
    {
        _context.Feeds.Update(feed);
        await _context.SaveChangesAsync();
        return Result.Ok(feed);
    }
}