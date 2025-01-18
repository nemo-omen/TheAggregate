using Microsoft.EntityFrameworkCore;
using TheAggregate.Api.Data;
using TheAggregate.Api.Features.Subscriptions.Types;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions;

public interface ISubscriptionsService
{
    Task<List<SubscriptionFeedResponse>> GetSubscriptions(string userId);
    Task<List<SubscriptionFeedItem>> GetSubscriptionItems(string userId);
    Task SubscribeToFeed(string userId, Guid feedId);
}

public class SubscriptionsService : ISubscriptionsService
{
    private readonly ApplicationDbContext _context;

    public SubscriptionsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubscriptionFeedResponse>> GetSubscriptions(string userId)
    {
        var subs = await _context.Subscriptions
            .AsNoTracking()
            .Where(x => string.Equals(x.UserId, userId))
            .Select(sub => new SubscriptionFeedResponse
            {
                Subscription = new SubscriptionResponse
                {
                    FeedId = sub.FeedId,
                    UserId = sub.UserId,
                    User = sub.User,
                    CreatedAt = sub.CreatedAt,
                    UpdatedAt = sub.UpdatedAt
                },
                SubscriptionFeed = new SubscriptionFeed {
                    Id = sub.Feed.Id,
                    Title = sub.Feed.Title,
                    Description = sub.Feed.Description,
                    FeedUrl = sub.Feed.FeedUrl,
                    WebUrl = sub.Feed.WebUrl,
                    Categories = sub.Feed.Categories,
                    Language = sub.Feed.Language,
                    Items = sub.Feed.Items.Select(item => new SubscriptionFeedItem
                    {
                        FeedItem = item,
                        IsRead = _context.UserStates
                            .Where(us => us.FeedItemId == item.Id && string.Equals(us.UserId, userId))
                            .Select(us => us.IsRead)
                            .FirstOrDefault(),
                        IsBookmarked = _context.UserStates
                            .Where(us => us.FeedItemId == item.Id && string.Equals(us.UserId, userId))
                            .Select(us => us.IsBookmarked)
                            .FirstOrDefault()
                    }).ToList()
                }
            })
            .ToListAsync();

        return subs;
    }

    public async Task<List<SubscriptionFeedItem>> GetSubscriptionItems(string userId)
    {
        var subscriptionFeedItems = new List<SubscriptionFeedItem>();

        var subscriptionFeedIds = await _context.Subscriptions
            .AsNoTracking()
            .Where(x => string.Equals(x.UserId, userId))
            .Select(x => x.FeedId)
            .ToListAsync();

        foreach (var feedId in subscriptionFeedIds)
        {
            var query = from f in _context.FeedItems
                join us in _context.UserStates
                    on new { FeedItemId = f.Id, UserId = userId }
                    equals new { us.FeedItemId, us.UserId } into userStateJoin
                from us in userStateJoin.DefaultIfEmpty()
                where f.FeedId == feedId
                select new SubscriptionFeedItem
                {
                    FeedItem = f,
                    IsRead = us.IsRead,
                    IsBookmarked = us.IsBookmarked,
                };

            var queryResult = await query
                .AsNoTracking()
                .ToListAsync();

            subscriptionFeedItems.AddRange(queryResult);
        }

        var orderedItems = subscriptionFeedItems
            .OrderByDescending(fi => fi.FeedItem.Published)
            .ToList();

        return orderedItems;
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