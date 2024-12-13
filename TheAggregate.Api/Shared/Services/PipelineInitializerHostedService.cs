using MediatR;
using TheAggregate.Api.Features.Feeds.GetFeeds;

namespace TheAggregate.Api.Shared.Services;

public class PipelineInitializerHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PipelineInitializerHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Initialize the news-gathering pipeline
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new GetFeedsCommand(), cancellationToken);
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}