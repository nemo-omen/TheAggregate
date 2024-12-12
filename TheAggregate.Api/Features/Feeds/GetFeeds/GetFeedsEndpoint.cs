using FastEndpoints;
using FluentResults;
using TheAggregate.Api.Features.Feeds.Types;

namespace TheAggregate.Api.Features.Feeds.GetFeeds;

public class GetFeedsEndpoint : EndpointWithoutRequest<GetFeedsResponse>
{
    public override void Configure()
    {
        Get("feeds");
        // allow anonymous for now
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var feedsResult = await new GetFeedsCommand()
        {
        }.ExecuteAsync(cancellationToken);

        if (feedsResult.IsFailed)
        {
            await SendStringAsync(
                feedsResult.Errors.First().Message, 500, cancellation: cancellationToken);
        }

        var feeds = new List<FeedResponse>();

        foreach (var feed in feedsResult.Value)
        {
            var feedResponse = new FeedResponse
            {
                Id = feed.Id,
                Title = feed.Title,
                Description = feed.Description,
                WebUrl = feed.WebUrl,
                FeedUrl = feed.FeedUrl,
                ImageUrl = feed.ImageUrl,
                Categories = feed.Categories,
                Items = new List<ItemResponse>()
            };

            foreach (var item in feed.Items)
            {
                var itemResponse = new ItemResponse
                {
                    Id = item.Id,
                    FeedId = item.FeedId,
                    Title = item.Title,
                    Author = item.Author,
                    Summary = item.Summary,
                    HtmlContent = item.HtmlContent,
                    ImageUrl = item.ImageUrl,
                    Url = item.Url,
                    PlainTextContent = item.PlainTextContent,
                    Published = item.Published,
                    Categories = item.Categories,
                };
                feedResponse.Items.Add(itemResponse);
            }
            feeds.Add(feedResponse);
        }

        var res = new GetFeedsResponse { Feeds = feeds };

        await SendAsync(res, cancellation: cancellationToken);
    }
}