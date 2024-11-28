using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public class GetFeedsCommand : ICommand<Result<List<Feed>>>
{
    // no data, just get all of the feeds
    // -- in the future, get all feeds for a user
}