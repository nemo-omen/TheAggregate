using FluentResults;
using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeed;

public record GetFeedQuery : IRequest<Result<Feed>>
{
    public Guid Id { get; init; }

    public GetFeedQuery(Guid id)
    {
        Id = id;
    }
}