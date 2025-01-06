using FluentResults;
using MediatR;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeed;

public class GetFeedQueryHandler : IRequestHandler<GetFeedQuery, Result<Feed>>
{
    private readonly IFeedsService _feedsService;

    public GetFeedQueryHandler(IFeedsService feedsService)
    {
        _feedsService = feedsService;
    }

    public async Task<Result<Feed>> Handle(GetFeedQuery request, CancellationToken cancellationToken)
    {
        return await _feedsService.GetFeedByIdAsync(request.Id);
    }
}