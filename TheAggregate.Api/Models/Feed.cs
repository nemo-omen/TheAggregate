namespace TheAggregate.Api.Models;

public class Feed : BaseModel
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string WebUrl { get; set; }
    public required string FeedUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? Language { get; set; }
    public List<string> Category { get; set; } = [];
    public List<FeedItem> Items { get; set; } = [];
}