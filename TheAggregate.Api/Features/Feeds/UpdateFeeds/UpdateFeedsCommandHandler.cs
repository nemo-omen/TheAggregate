using MediatR;
using TheAggregate.Api.Features.Feeds.GetFeeds;
using TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.Feeds.UpdateFeeds;

public class UpdateFeedsCommandHandler : IRequestHandler<UpdateFeedsCommand>
{
    private readonly IMediator _mediator;

    public UpdateFeedsCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UpdateFeedsCommand command, CancellationToken cancellationToken)
    {
        Banner.Log($"[UpdateFeedsCommandHandler] - Updating feeds");
        var feedsResult = await _mediator.Send(new GetFeedsCommand(), cancellationToken);

        if(!feedsResult.IsSuccess)
        {
            Banner.Log($"[UpdateFeedsCommandHandler] - Failed to get feeds: {feedsResult}");
        }

        var feeds = feedsResult.Value;
        await _mediator.Send(new GetSyndicationFeedsCommand(feeds), cancellationToken);
    }
}