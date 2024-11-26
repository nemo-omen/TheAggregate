using System.ComponentModel.DataAnnotations;

namespace TheAggregate.Api.Models;

public class Collection : BaseModel
{
    [MaxLength(255)]
    public required string Title { get; set; }
    [MaxLength(1023)]
    public string? Description { get; set; }
    [MaxLength(255)]
    public required string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public List<CollectionItem> Items { get; set; } = [];
}