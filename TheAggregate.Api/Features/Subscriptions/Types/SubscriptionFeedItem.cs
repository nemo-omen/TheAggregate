using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions.Types;

public record SubscriptionFeedItem
{
    public FeedItem FeedItem { get; init; }
    public bool IsRead { get; init; }
    public bool IsBookmarked { get; init; }
}