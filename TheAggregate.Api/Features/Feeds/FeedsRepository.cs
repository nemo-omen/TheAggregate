using System.Data.Common;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Exceptions;

namespace TheAggregate.Api.Features.Feeds;

public interface IFeedsRepository
{
    Task<Result<List<Feed>>> GetFeedsAsync();
    Task<Feed> GetFeedByIdAsync(Guid id);
    Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl);
    Task<Result<List<Feed>>> UpdateFeedsAsync(List<Feed> feeds);
    // Task<Result<Feed>> CreateFeedAsync(Feed feed);
    Task<Result<Feed>> UpdateFeedAsync(Feed feed);
    Task<Result<List<Feed>>> SearchFeeds(string searchTerm);
    Task<Result<List<FeedItem>>> SearchItems(string searchTerm);
    // Task<Result<Feed>> DeleteFeedAsync(int id);
    Task<FeedItem> GetFeedItemByIdAsync(Guid id);
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
            .Include(f => f.Items.OrderByDescending(i => i.Published))
            .OrderBy(f => f.Title)
            .ToListAsync();
        
        return Result.Ok(feeds);
    }

    public async Task<Feed> GetFeedByIdAsync(Guid id)
    {
        var feed = await _context.Feeds
            .AsNoTracking()
            .Include(f => f.Items.OrderByDescending(i => i.Published))
            .FirstOrDefaultAsync(f => f.Id == id);

        if (feed is null)
        {
            throw new NotFoundException("Not found");
        }

        return feed;
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

    public async Task<Result<List<Feed>>> SearchFeeds(string searchTerm)
    {
        List<Feed> feeds;
        try
        {
            feeds = await _context.Feeds
                .Where(fi => fi.SearchVector.Matches(searchTerm))
                .ToListAsync();
        }
        catch (DbException e)
        {
            return Result.Fail<List<Feed>>("[FeedsRepository.SearchFeeds] exception: " + e.Message);
        }

        return Result.Ok(feeds);
    }

    public async Task<Result<List<FeedItem>>> SearchItems(string searchTerm)
    {
        List<FeedItem> feedItems;
        try
        {
            feedItems = await _context.FeedItems
                .Where(fi => fi.SearchVector.Matches(searchTerm))
                .ToListAsync();
        }
        catch (DbException e)
        {
            return Result.Fail<List<FeedItem>>("[FeedsRepository.SearchItems] exception: " + e.Message);
        }

        return Result.Ok(feedItems);
    }

    public async Task<FeedItem> GetFeedItemByIdAsync(Guid id)
    {
        var item = await _context.FeedItems
            .AsNoTracking()
            .Include(fi => fi.Feed)
            .FirstOrDefaultAsync(fi => fi.Id == id);

        if (item is null)
        {
            throw new NotFoundException($"Feed with id {id} not found");
        }

        return item;
    }
}