using System.ServiceModel.Syndication;
using FluentResults;
using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsCommand : IRequest<Result<List<SyndicationFeed>>>
{
    public List<Feed> Feeds { get; set; }
    public GetSyndicationFeedsCommand(List<Feed> feeds)
    {
        Feeds = feeds;
    }
}