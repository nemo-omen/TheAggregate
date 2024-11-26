using Microsoft.EntityFrameworkCore;

namespace TheAggregate.Api.Models;

[Index(nameof(CollectionId), nameof(FeedItemId), IsUnique = true)]
public class CollectionItem : BaseModel
{
    public Guid CollectionId { get; set; }
    public Guid FeedItemId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}