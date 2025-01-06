using MediatR;

namespace TheAggregate.Api.Features.Subscriptions.SubscribeToFeed;

public record SubscribeToFeedCommand : IRequest
{
    public string UserId { get; init; }
    public Guid FeedId { get; init; }
}