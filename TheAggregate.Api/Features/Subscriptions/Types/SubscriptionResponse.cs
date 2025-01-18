using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions.Types;

public record SubscriptionResponse
{
    public string UserId { get; init; }
    public ApplicationUser User { get; init; }
    public Guid FeedId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}