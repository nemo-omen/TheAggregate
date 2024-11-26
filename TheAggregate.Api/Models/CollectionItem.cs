namespace TheAggregate.Api.Models;

public class CollectionItem : BaseModel
{
    public Guid CollectionId { get; set; }
    public Guid FeedItemId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}