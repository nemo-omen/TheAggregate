using System.ServiceModel.Syndication;
using FluentResults;
using MediatR;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsCommandHandler : IRequestHandler<GetSyndicationFeedsCommand, Result<List<SyndicationFeed>>>
{
    private readonly IAggregationService _aggregationService;
    private readonly IMediator _mediator;
    
    public GetSyndicationFeedsCommandHandler(IAggregationService aggregationService, IMediator mediator)
    {
        _aggregationService = aggregationService;
        _mediator = mediator;
    }
    
    public async Task<Result<List<SyndicationFeed>>> Handle(GetSyndicationFeedsCommand command,
        CancellationToken cancellationToken)
    {
        Banner.Log($"[GetSyndicationFeedsCommandHandler] - Getting SyndicationFeeds for {command.Feeds.Count} feeds");
        var syndicationFeedsResults = await _aggregationService
            .GetSyndicationFeedsFromFeedsAsync(command.Feeds);

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
        await _mediator.Publish(new GetSyndicationFeedsEvent { SyndicationFeeds = feeds }, cancellationToken);
        return Result.Ok(feeds);
    }
}