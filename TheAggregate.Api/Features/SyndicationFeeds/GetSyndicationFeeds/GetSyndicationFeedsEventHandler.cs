using MediatR;
using TheAggregate.Api.Features.Feeds.SaveFeedsFromSyndication;

namespace TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

public class GetSyndicationFeedsEventHandler : INotificationHandler<GetSyndicationFeedsEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetSyndicationFeedsEventHandler> _logger;
    
    public GetSyndicationFeedsEventHandler(IMediator mediator, ILogger<GetSyndicationFeedsEventHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task Handle(GetSyndicationFeedsEvent e, CancellationToken ct)
    {
        _logger.LogInformation($"[GetSyndicationFeedsEventHandler] Handling {e.SyndicationFeeds.Count} syndication feeds");

        await _mediator.Send(new SaveFeedsFromSyndicationCommand(e.SyndicationFeeds), ct);
    }
}