using MediatR;
using TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsEventHandler : INotificationHandler<GetFeedsEvent>
{
    private readonly IMediator _mediator;

    public GetFeedsEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(GetFeedsEvent e, CancellationToken ct)
    {
        // do something with the feeds
        var getSyndicationFeedsCommandResult = await _mediator
            .Send(new GetSyndicationFeedsCommand(e.Feeds), ct);
    }
}