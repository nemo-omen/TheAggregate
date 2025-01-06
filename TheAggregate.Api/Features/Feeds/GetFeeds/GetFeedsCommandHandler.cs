using FluentResults;
using MediatR;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsCommandHandler : IRequestHandler<GetFeedsCommand, Result<List<Feed>>>
{
    private readonly IFeedsService _feedsService;

    public GetFeedsCommandHandler(IFeedsService feedsService)
    {
        _feedsService = feedsService;
    }

    public async Task<Result<List<Feed>>> Handle(GetFeedsCommand command,
        CancellationToken cancellationToken)
    {
        Banner.Log($"[GetFeedsCommandHandler] - Getting feeds");
        var feedsResult = await _feedsService.GetFeedsAsync();
        
        return Result.Ok(feedsResult.Value);
    }
}