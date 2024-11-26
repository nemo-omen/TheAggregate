using System.ComponentModel.DataAnnotations;

namespace TheAggregate.Api.Models;

public class Feed : BaseModel
{
    [MaxLength(255)]
    public required string Title { get; set; }
    [MaxLength(255)]
    public required string Description { get; set; }
    [MaxLength(255)]
    public required string WebUrl { get; set; }
    [MaxLength(255)]
    public required string FeedUrl { get; set; }
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    [MaxLength(255)]
    public string? Language { get; set; }
    public List<string> Categories { get; set; } = [];
    public List<FeedItem> Items { get; set; } = [];
}