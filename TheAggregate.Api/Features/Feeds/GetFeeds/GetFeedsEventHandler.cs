using FastEndpoints;
using TheAggregate.Api.Features.FeedAggregation;
using TheAggregate.Api.Features.SyndicationFeeds.GetSyndicationFeeds;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsEventHandler : IEventHandler<GetFeedsEvent>
{
    public Task HandleAsync(GetFeedsEvent e, CancellationToken ct)
    {
        // do something with the feeds
        var getSyndicationFeedsCommandResult = new GetSyndicationFeedsCommand(e.Feeds)
            .ExecuteAsync(ct);
        
        return Task.CompletedTask;
    }
}