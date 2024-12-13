using FluentResults;
using MediatR;
using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.Search;

public class SearchQueryHandler : IRequestHandler<SearchQuery, Result<SearchResponse>>
{
    private readonly IFeedsService _feedsService;
    private readonly ILogger<SearchQueryHandler> _logger;

    public SearchQueryHandler(IFeedsService feedsService, ILogger<SearchQueryHandler> logger)
    {
        _feedsService = feedsService;
        _logger = logger;
    }

    public async Task<Result<SearchResponse>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var result = await _feedsService.SearchAsync(request.SearchTerm);
        if (result.IsFailed) return Result.Fail<SearchResponse>(result.Errors);
        var response = new SearchResponse
        {
            Count = result.Value.Count,
            Items = result.Value,
        };
        return response;
    }
}