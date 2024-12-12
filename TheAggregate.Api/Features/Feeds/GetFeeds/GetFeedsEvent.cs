using FastEndpoints;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public class GetFeedsEvent : IEvent
{
    public List<Feed> Feeds { get; set; }
    
    public GetFeedsEvent()
    {
    }
    
    public GetFeedsEvent(List<Feed> feeds)
    {
        Feeds = feeds;
    }
}