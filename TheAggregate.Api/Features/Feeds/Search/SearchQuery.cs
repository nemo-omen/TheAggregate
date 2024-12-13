using FluentResults;
using MediatR;
using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds.Search;

public class SearchQuery : IRequest<Result<SearchResponse>>
{
    public string SearchTerm { get; set; }

    public SearchQuery(string searchTerm)
    {
        SearchTerm = searchTerm;
    }
}