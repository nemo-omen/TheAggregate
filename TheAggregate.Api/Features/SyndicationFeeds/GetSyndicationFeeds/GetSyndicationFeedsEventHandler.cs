using FastEndpoints;
using TheAggregate.Api.Features.Feeds;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsEventHandler : IEventHandler<GetSyndicationFeedsEvent>
{
    private readonly ILogger<GetSyndicationFeedsEventHandler> _logger;
    
    public GetSyndicationFeedsEventHandler(ILogger<GetSyndicationFeedsEventHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task HandleAsync(GetSyndicationFeedsEvent e, CancellationToken ct)
    {
        _logger.LogInformation($"[GetSyndicationFeedsEventHandler] Handling {e.SyndicationFeeds.Count} syndication feeds");
        await new SaveFeedsFromSyndicationCommand
        {
            SyndicationFeeds = e.SyndicationFeeds
        }.ExecuteAsync(ct);
    }
}