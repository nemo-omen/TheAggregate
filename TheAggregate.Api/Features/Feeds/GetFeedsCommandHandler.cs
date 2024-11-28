using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public class GetFeedsCommandHandler : ICommandHandler<GetFeedsCommand, Result<List<Feed>>>
{
    private readonly IFeedsService _feedsService;

    public GetFeedsCommandHandler(IFeedsService feedsService)
    {
        _feedsService = feedsService;
    }

    public async Task<Result<List<Feed>>> ExecuteAsync(GetFeedsCommand command,
        CancellationToken cancellationToken)
    {
        var feedsResult = await _feedsService.GetFeedsAsync();

        if (feedsResult.IsSuccess)
        {
            await new GetFeedsEvent(feedsResult.Value)
                .PublishAsync(Mode.WaitForAll, cancellationToken);
        }
        
        return Result.Ok(feedsResult.Value);
    }
}