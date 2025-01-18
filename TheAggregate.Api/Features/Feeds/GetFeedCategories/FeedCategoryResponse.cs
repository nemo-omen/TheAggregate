namespace TheAggregate.Api.Features.Feeds.GetFeedCategories;

public record FeedCategoryResponse
{
    public string Category { get; init; }
    public string CategoryImage { get; init; }
}