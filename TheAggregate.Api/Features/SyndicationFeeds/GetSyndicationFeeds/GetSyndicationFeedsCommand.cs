using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsCommand : ICommand<Result<List<SyndicationFeed>>>
{
    public List<Feed> Feeds { get; set; }
    public GetSyndicationFeedsCommand(List<Feed> feeds)
    {
        Feeds = feeds;
    }
}