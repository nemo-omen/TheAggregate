using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public static class FeedMapper
{
    public static FeedResponse MapFeedToFeedResponse(Feed feed)
    {
        return new FeedResponse
        {
            Id = feed.Id,
            Title = feed.Title,
            Description = feed.Description,
            FeedUrl = feed.FeedUrl,
            WebUrl = feed.WebUrl,
            ImageUrl = feed.ImageUrl,
            Language = feed.Language,
            Categories = feed.Categories,
            Items = feed.Items.Select(MapItemToItemResponse).ToList(),
        };
    }

    public static ItemResponse MapItemToItemResponse(FeedItem item)
    {
        return new ItemResponse
        {
            Id = item.Id,
            Title = item.Title,
            Url = item.Url,
            ImageUrl = item.ImageUrl,
            Summary = item.Summary,
            PlainTextContent = item.PlainTextContent,
            HtmlContent = item.HtmlContent,
            Published = item.Published,
            Author = item.Author,
            FeedId = item.FeedId,
            Categories = item.Categories,
        };
    }
}