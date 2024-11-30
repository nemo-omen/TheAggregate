using System.ServiceModel.Syndication;
using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.Feeds;

public class SaveFeedsFromSyndicationCommandHandler : ICommandHandler<SaveFeedsFromSyndicationCommand, List<Result<Feed>>>
{
    private readonly IFeedsService _feedsService;
    
    public SaveFeedsFromSyndicationCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _feedsService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IFeedsService>();
    }
    
    public async Task<List<Result<Feed>>> ExecuteAsync(SaveFeedsFromSyndicationCommand command, CancellationToken cancellationToken)
    {
        Banner.Log($"Saving items for {command.SyndicationFeeds.Count()} feeds.");
        var saveResult = await _feedsService.UpdateFeedItemsFromSyndicationAsync(command.SyndicationFeeds);
        Banner.Log($"Successfully saved items for {saveResult.Count(r => r.IsSuccess)} feeds.");
        Banner.Log($"Failed to save items for {saveResult.Count(r => r.IsFailed)} feeds.");
        return saveResult;
    }
}