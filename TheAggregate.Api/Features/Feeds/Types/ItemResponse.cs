namespace TheAggregate.Api.Features.Feeds.Types;

public record ItemResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? PlainTextContent { get; set; }
    public string? HtmlContent { get; set; }
    public required string Url { get; set; }
    public string? Summary { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime Published { get; set; }
    public string? Author { get; set; }
    public Guid FeedId { get; set; }
    public ICollection<string> Categories { get; set; } = [];
}