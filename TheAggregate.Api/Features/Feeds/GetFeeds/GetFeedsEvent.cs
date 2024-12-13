using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsEvent : INotification
{
    public List<Feed> Feeds { get; set; }
    public GetFeedsEvent(List<Feed> feeds)
    {
        Feeds = feeds;
    }
}