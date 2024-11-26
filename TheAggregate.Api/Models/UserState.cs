namespace TheAggregate.Api.Models;

public class UserState : BaseModel
{
    public string UserId { get; set; }
    public Guid FeedItemId { get; set; }
    public bool IsRead { get; set; }
    public bool IsBookmarked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}