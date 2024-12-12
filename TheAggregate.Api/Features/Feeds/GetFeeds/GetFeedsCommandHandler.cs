using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsCommandHandler : ICommandHandler<GetFeedsCommand, Result<List<Feed>>>
{
    private readonly IFeedsService _feedsService;

    public GetFeedsCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _feedsService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IFeedsService>();
    }

    public async Task<Result<List<Feed>>> ExecuteAsync(GetFeedsCommand command,
        CancellationToken cancellationToken)
    {
        Banner.Log($"[GetFeedsCommandHandler] - Getting feeds");
        var feedsResult = await _feedsService.GetFeedsAsync();

        if (feedsResult.IsSuccess)
        {
            await new GetFeedsEvent(feedsResult.Value)
                .PublishAsync(Mode.WaitForAll, cancellationToken);
        }
        
        return Result.Ok(feedsResult.Value);
    }
}