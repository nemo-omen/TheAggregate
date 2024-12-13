using TheAggregate.Api.Features.Feeds.Types;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsResponse
{
    public List<FeedResponse> Feeds { get; set; } = [];
}