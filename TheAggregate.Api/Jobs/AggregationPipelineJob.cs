using Hangfire;
using MediatR;
using TheAggregate.Api.Features.Feeds.GetFeeds;

namespace TheAggregate.Api.Jobs;

public class AggregationPipelineJob
{
    private readonly IMediator _mediator;

    public AggregationPipelineJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task StartPipeline()
    {
        BackgroundJob.Enqueue(() => SendPipelineCommand());
    }

    public async Task SendPipelineCommand()
    {
        await _mediator.Send(new GetFeedsCommand());
    }
}