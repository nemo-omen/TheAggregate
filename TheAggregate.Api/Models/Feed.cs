using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace TheAggregate.Api.Models;

[Index(nameof(WebUrl), IsUnique = true)]
[Index(nameof(FeedUrl), IsUnique = true)]
[Index(nameof(Title))]
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
    public NpgsqlTsVector? SearchVector { get; set; }
}