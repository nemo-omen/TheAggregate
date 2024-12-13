using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.Types;

public record SearchResponse
{
    public int Count { get; set; }
    public List<FeedItem> Items { get; init; } = [];
}