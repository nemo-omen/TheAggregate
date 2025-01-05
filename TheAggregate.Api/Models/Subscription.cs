namespace TheAggregate.Api.Models;

public class Subscription : BaseModel
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid FeedId { get; set; }
    public Feed Feed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}