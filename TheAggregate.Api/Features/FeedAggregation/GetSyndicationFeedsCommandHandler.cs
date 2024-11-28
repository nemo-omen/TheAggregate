using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;

namespace TheAggregate.Api.Features.FeedAggregation;

public class GetSyndicationFeedsCommandHandler 
    : ICommandHandler<GetSyndicationFeedsCommand, Result<List<SyndicationFeed>>>
{
    private readonly IAggregationService _aggregationService;
    
    public GetSyndicationFeedsCommandHandler(IAggregationService aggregationService)
    {
        _aggregationService = aggregationService;
    }
    
    public async Task<Result<List<SyndicationFeed>>> ExecuteAsync(GetSyndicationFeedsCommand command,
        CancellationToken cancellationToken)
    {
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
        
        return Result.Ok(feeds);
    }
}