using System.ServiceModel.Syndication;
using MediatR;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsEvent : INotification
{
    public List<SyndicationFeed> SyndicationFeeds { get; set; }
}