using FluentResults;
using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeedItem;

public record GetFeedItemQuery : IRequest<Result<FeedItem>>
{
    public Guid Id { get; init; }

    public GetFeedItemQuery(Guid id)
    {
        Id = id;
    }
}