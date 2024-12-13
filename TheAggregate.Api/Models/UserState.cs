using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TheAggregate.Api.Models;

[Index(nameof(UserId), nameof(FeedItemId), IsUnique = true)]
public class UserState : BaseModel
{
    [MaxLength(255)]
    public required string UserId { get; set; }
    public Guid FeedItemId { get; set; }
    public bool IsRead { get; set; }
    public bool IsBookmarked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}