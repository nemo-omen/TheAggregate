using FluentResults;
using MediatR;
using NuGet.Protocol.Plugins;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.GetFeedItem;

public class GetFeedItemQueryHandler : IRequestHandler<GetFeedItemQuery, Result<FeedItem>>
{
    private readonly IFeedsService _feedsService;

    public GetFeedItemQueryHandler(IFeedsService feedsService)
    {
        _feedsService = feedsService;
    }

    public async Task<Result<FeedItem>> Handle(GetFeedItemQuery request, CancellationToken cancellationToken)
    {
        return await _feedsService.GetFeedItemByIdAsync(request.Id);
    }
}