using System.ServiceModel.Syndication;
using FluentResults;
using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.SaveFeedsFromSyndication;

public class SaveFeedsFromSyndicationCommand : IRequest<List<Result<Feed>>>
{
    public List<SyndicationFeed> SyndicationFeeds { get; set; }

    public SaveFeedsFromSyndicationCommand(List<SyndicationFeed> syndicationFeeds)
    {
        SyndicationFeeds = syndicationFeeds;
    }
}