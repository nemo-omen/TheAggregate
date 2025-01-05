using FluentResults;
using MediatR;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsCommandHandler : IRequestHandler<GetFeedsCommand, Result<List<Feed>>>
{
    private readonly IMediator _mediator;
    private readonly IFeedsService _feedsService;

    public GetFeedsCommandHandler(IFeedsService feedsService, IMediator mediator)
    {
        _mediator = mediator;
        _feedsService = feedsService;
    }

    public async Task<Result<List<Feed>>> Handle(GetFeedsCommand command,
        CancellationToken cancellationToken)
    {
        Banner.Log($"[GetFeedsCommandHandler] - Getting feeds");
        var feedsResult = await _feedsService.GetFeedsAsync();

        // if (feedsResult.IsSuccess)
        // {
        //     await _mediator.Publish(new GetFeedsEvent(feedsResult.Value), cancellationToken);
        // }
        
        return Result.Ok(feedsResult.Value);
    }
}