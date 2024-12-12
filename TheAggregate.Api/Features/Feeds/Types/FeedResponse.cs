namespace TheAggregate.Api.Features.Feeds.Types;

public record FeedResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string WebUrl { get; set; }
    public required string FeedUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? Language { get; set; }
    public List<ItemResponse> Items { get; set; } = [];
    public List<string> Categories { get; set; } = [];
}