namespace TheAggregate.Api.Models;

public class FeedItem : BaseModel
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required string Url { get; set; }
    public string? Summary { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime Published { get; set; }
    public string? Author { get; set; }
    public string? FeedId { get; set; }
    public Feed Feed { get; set; }
}