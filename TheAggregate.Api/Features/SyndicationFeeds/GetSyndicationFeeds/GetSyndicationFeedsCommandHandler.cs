using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Features.FeedAggregation;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsCommandHandler 
    : ICommandHandler<GetSyndicationFeedsCommand, Result<List<SyndicationFeed>>>
{
    private readonly IAggregationService _aggregationService;
    
    public GetSyndicationFeedsCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _aggregationService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IAggregationService>();
    }
    
    public async Task<Result<List<SyndicationFeed>>> ExecuteAsync(GetSyndicationFeedsCommand command,
        CancellationToken cancellationToken)
    {
        Banner.Log($"[GetSyndicationFeedsCommandHandler] - Getting SyndicationFeeds for {command.Feeds.Count} feeds");
        var syndicationFeedsResults = await _aggregationService.GetSyndicationFeedsFromFeedsAsync(command.Feeds);

        var feeds = new List<SyndicationFeed>();
        
        foreach (var syndicationFeedsResult in syndicationFeedsResults)
        {
            // Todo: Decide what to do with failures
            if (syndicationFeedsResult.IsSuccess)
            {
                feeds.Add(syndicationFeedsResult.Value);
            }
        }
        Banner.Log($"[GetSyndicationFeedsCommandHandler] - Got {feeds.Count} SyndicationFeeds");
        await new GetSyndicationFeedsEvent
        {
            SyndicationFeeds = feeds,
        }.PublishAsync(Mode.WaitForAll, cancellationToken);
        return Result.Ok(feeds);
    }
}