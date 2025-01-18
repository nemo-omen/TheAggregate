using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions.Types;

public record SubscriptionFeedResponse
{
    public SubscriptionResponse Subscription { get; set; }
    public SubscriptionFeed SubscriptionFeed { get; set; }
}