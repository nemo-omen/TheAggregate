using System.ServiceModel.Syndication;
using FluentResults;
using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Util;
using TheAggregate.Shared.Util;

namespace TheAggregate.Api.Features.Feeds;

public interface IFeedsService
{
    Task<Result<List<Feed>>> GetFeedsAsync();
    Task<Result<Feed>> GetFeedByIdAsync(int id);
    Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl);
    Task<List<Result<Feed>>> UpdateFeedItemsFromSyndicationAsync(List<SyndicationFeed> feeds);

    Task<Result<List<ItemResponse>>> SearchAsync(string searchTerm);
    // Task<Result<Feed>> CreateFeedAsync(Feed feed);
    // Task<Result<Feed>> UpdateFeedAsync(Feed feed);
    // Task<Result<Feed>> DeleteFeedAsync(int id);
}

public class FeedsService : IFeedsService
{
    private readonly IFeedsRepository _feedsRepository;
    private readonly ILogger<FeedsService> _logger;

    public FeedsService(IFeedsRepository feedsRepository, ILogger<FeedsService> logger)
    {
        _feedsRepository = feedsRepository;
        _logger = logger;
    }

    public async Task<Result<List<Feed>>> GetFeedsAsync()
    {
        return await _feedsRepository.GetFeedsAsync();
    }

    public async Task<Result<Feed>> GetFeedByIdAsync(int id)
    {
        return await _feedsRepository.GetFeedByIdAsync(id);
    }

    public async Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl)
    {
        return await _feedsRepository.GetFeedByFeedUrlAsync(feedUrl);
    }

    public async Task<List<Result<Feed>>> UpdateFeedItemsFromSyndicationAsync(List<SyndicationFeed> syndicationFeeds)
    {
        var feedsResult = await _feedsRepository.GetFeedsAsync();
        if (feedsResult.IsFailed)
        {
            return new List<Result<Feed>>() { Result.Fail<Feed>("Failed to get feeds") };
        }

        _logger.LogInformation($"[TheAggregate] Got: {feedsResult.Value.Count} feeds");


        var feeds = new List<Result<Feed>>();
        // var updateFeeds = new List<Feed>();
        foreach (var syndicationFeed in syndicationFeeds)
        {
            _logger.LogInformation(
                $"[TheAggregate] Saving items from SyndicationFeed: {syndicationFeed.Links[0].Uri.ToString()}");
            var feed = feedsResult.Value.FirstOrDefault(f =>
                           syndicationFeed.Links.Any(l =>
                               l.Uri.Authority == new Uri(f.WebUrl).Authority)) ??
                       feedsResult.Value.FirstOrDefault(f =>
                           syndicationFeed.Links.Any(l =>
                               l.Uri.Authority == new Uri(f.FeedUrl).Authority));

            if (feed is null)
            {
                feeds.Add(Result.Fail<Feed>($"Feed not found: {syndicationFeed.Links[0].Uri.OriginalString}"));
            }

            _logger.LogInformation($"[TheAggregate] Found feed: {feed.Title}");

            // update items
            foreach (var syndicationItem in syndicationFeed.Items)
            {
                var savedItem = feed.Items.FirstOrDefault(i =>
                    UrlUtil.NormalizeUrl(i.Url) == UrlUtil.NormalizeUrl(syndicationItem.Links[0].Uri.OriginalString));

                var author = syndicationItem.Authors?.FirstOrDefault()?.Name ?? "";
                var summary = syndicationItem.Summary?.Text ?? "";
                var categories = syndicationItem.Categories?.Select(c => c.Name).ToList();

                var isSummaryHtml = HtmlUtils.IsHtmlString(summary);
                string html, plainText;
                if (isSummaryHtml)
                {
                    html = HtmlUtils.CleanWhitespace(summary);
                    plainText = HtmlUtils.GetPlainTextFromHtml(html);
                }
                else
                {
                    plainText = HtmlUtils.CleanWhitespace(summary);
                    html = HtmlUtils.WrapPlainText(plainText);
                }

                if (savedItem is null)
                {
                    var newItem = new FeedItem
                    {
                        Title = syndicationItem.Title.Text,
                        Author = author,
                        Url = syndicationItem.Links[0].Uri.OriginalString,
                        HtmlContent = html,
                        PlainTextContent = plainText,
                        Summary = summary,
                        FeedId = feed.Id,
                        Published = syndicationItem.PublishDate.Date.ToUniversalTime(),
                        Categories = categories ?? [],
                    };
                    _logger.LogInformation($"[TheAggregate] Created FeedItem: {newItem.Title}");
                    feed.Items.Add(newItem);
                }
                else
                {
                    // only save contents if they are different

                    savedItem.HtmlContent = string.Equals(savedItem.HtmlContent, html) ? savedItem.HtmlContent : html;
                    savedItem.PlainTextContent = string.Equals(savedItem.PlainTextContent, plainText)
                        ? savedItem.PlainTextContent
                        : plainText;

                    savedItem.Summary = string.Equals(savedItem.Summary, summary) ? savedItem.Summary : summary;
                    savedItem.Published = (savedItem.Published == syndicationItem.PublishDate.Date.ToUniversalTime())
                        ? savedItem.Published
                        : syndicationItem.PublishDate.Date.ToUniversalTime();
                }
            }

            await _feedsRepository.UpdateFeedAsync(feed);
            feeds.Add(Result.Ok(feed)!);
        }

        return feeds;
    }

    public async Task<Result<List<ItemResponse>>> SearchAsync(string searchTerm)
    {
        var itemsSearchResult = await _feedsRepository.SearchItems(searchTerm);
        if (itemsSearchResult.IsFailed) return Result.Fail<List<ItemResponse>>("Failed to search");
        var itemResponses = itemsSearchResult.Value.Select(FeedMapper.MapItemToItemResponse).ToList();
        return itemResponses;
    }
}