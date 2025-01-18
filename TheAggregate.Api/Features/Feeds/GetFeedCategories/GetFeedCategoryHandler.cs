using FluentResults;
using MediatR;

namespace TheAggregate.Api.Features.Feeds.GetFeedCategories;

public class GetFeedCategoryHandler : IRequestHandler<GetFeedCategoriesCommand, Result<List<FeedCategoryResponse>>>
{
    private readonly IFeedsService _feedsService;

    public GetFeedCategoryHandler(IFeedsService feedsService)
    {
        _feedsService = feedsService;
    }

    public async Task<Result<List<FeedCategoryResponse>>> Handle(
        GetFeedCategoriesCommand request, CancellationToken cancellationToken)
    {
        var categories = await _feedsService.GetFeedCategoriesAsync();
        return categories.Value;
    }
}