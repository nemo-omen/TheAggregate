using System.ServiceModel.Syndication;
using FastEndpoints;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsEvent : IEvent
{
    public List<SyndicationFeed> SyndicationFeeds { get; set; }
}