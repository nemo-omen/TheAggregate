using FluentResults;
using MediatR;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Util;

namespace TheAggregate.Api.Features.Feeds.SaveFeedsFromSyndication;

public class SaveFeedsFromSyndicationCommandHandler : IRequestHandler<SaveFeedsFromSyndicationCommand, List<Result<Feed>>>
{
    private readonly IFeedsService _feedsService;
    
    public SaveFeedsFromSyndicationCommandHandler(IFeedsService feedsService)
    {
        _feedsService = feedsService;
    }
    
    public async Task<List<Result<Feed>>> Handle(SaveFeedsFromSyndicationCommand command, CancellationToken cancellationToken)
    {
        Banner.Log($"Saving items for {command.SyndicationFeeds.Count()} feeds.");
        var saveResult = await _feedsService.UpdateFeedItemsFromSyndicationAsync(command.SyndicationFeeds);
        Banner.Log($"Successfully saved items for {saveResult.Count(r => r.IsSuccess)} feeds.");
        Banner.Log($"Failed to save items for {saveResult.Count(r => r.IsFailed)} feeds.");
        return saveResult;
    }
}