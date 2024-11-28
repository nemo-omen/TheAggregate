using FastEndpoints;
using TheAggregate.Api.Features.Feeds;

namespace TheAggregate.Api.Features.FeedAggregation;

public class GetFeedsEventHandler : IEventHandler<GetFeedsEvent>
{
    public Task HandleAsync(GetFeedsEvent e, CancellationToken ct)
    {
        // do something with the feeds
        var getSyndicationFeedsCommand = new GetSyndicationFeedsCommand(e.Feeds)
            .ExecuteAsync(ct);
        
        return Task.CompletedTask;
    }
}