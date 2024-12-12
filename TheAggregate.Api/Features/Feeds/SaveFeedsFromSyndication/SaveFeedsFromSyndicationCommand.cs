using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public class SaveFeedsFromSyndicationCommand : ICommand<List<Result<Feed>>>
{
    public List<SyndicationFeed> SyndicationFeeds { get; set; }
}