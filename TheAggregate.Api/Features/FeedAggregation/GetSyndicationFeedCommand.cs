using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;

namespace TheAggregate.Api.Features.FeedAggregation;

public class GetSyndicationFeedCommand : ICommand<Result<SyndicationFeed>>
{
    public required string Url { get; set; }
}