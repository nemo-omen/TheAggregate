using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TheAggregate.Api.Models;

[Index(nameof(FeedId), nameof(Title), IsUnique = true)]
public class FeedItem : BaseModel
{
    [MaxLength(255)]
    public required string Title { get; set; }
    public string? Content { get; set; }
    [MaxLength(255)]
    public required string Url { get; set; }
    [MaxLength(1023)]
    public string? Summary { get; set; }
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    public DateTime Published { get; set; }
    [MaxLength(255)]
    public string? Author { get; set; }
    [MaxLength(255)]
    public Guid FeedId { get; set; }
    public Feed Feed { get; set; }
}