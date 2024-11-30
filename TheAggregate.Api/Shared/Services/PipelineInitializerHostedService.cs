using FastEndpoints;
using TheAggregate.Api.Features.Feeds;

namespace TheAggregate.Api.Shared.Services;

public class PipelineInitializerHostedService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Initialize the news-gathering pipeline
        await new GetFeedsCommand().QueueJobAsync(
            expireOn: DateTime.UtcNow.AddSeconds(45),
            ct: cancellationToken
        );
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}