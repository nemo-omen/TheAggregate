namespace TheAggregate.Api.Features.Subscriptions.Types;

public record SubscriptionFeed
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string WebUrl { get; init; }
    public required string FeedUrl { get; init; }
    public string? ImageUrl { get; init; }
    public string? Language { get; init; }
    public List<string> Categories { get; init; } = [];
    public List<SubscriptionFeedItem> Items { get; init; } = [];
}