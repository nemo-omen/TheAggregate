using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsResponse
{
    public List<FeedResponse> Feeds { get; set; } = [];
}