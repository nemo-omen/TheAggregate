using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions;

public interface ISubscriptionsService
{
    Task<List<Subscription>> GetSubscriptions(string userId);
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
}