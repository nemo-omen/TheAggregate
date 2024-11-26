namespace TheAggregate.Api.Models;

public class Collection : BaseModel
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string UserId { get; set; }
}