using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions;

public interface ISubscriptionsService
{
    Task<List<Subscription>> GetSubscriptions(string userId);
    Task SubscribeToFeed(string userId, Guid feedId);
}

public class SubscriptionsService : ISubscriptionsService
{
    private readonly ApplicationDbContext _context;

    public SubscriptionsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subscription>> GetSubscriptions(string userId)
    {
        return await _context.Subscriptions
            .AsNoTracking()
            .Include(s => s.FeedId)
            .AsSplitQuery()
            .Where(x => string.Equals(x.UserId, userId))
            .ToListAsync();
    }

    public async Task SubscribeToFeed(string userId, Guid feedId)
    {
        var subscription = new Subscription
        {
            UserId = userId,
            FeedId = feedId
        };

        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }
}