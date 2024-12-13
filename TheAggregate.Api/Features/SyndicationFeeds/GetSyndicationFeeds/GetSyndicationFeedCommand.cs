using System.ServiceModel.Syndication;
using FluentResults;
using MediatR;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedCommand : IRequest<Result<SyndicationFeed>>
{
    public required string Url { get; set; }
}