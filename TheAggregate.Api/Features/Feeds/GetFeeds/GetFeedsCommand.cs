using FluentResults;
using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsCommand : IRequest<Result<List<Feed>>>
{
    // no data, just get all of the feeds
    // -- in the future, get all feeds for a user
}