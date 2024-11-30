using System.ServiceModel.Syndication;
using FluentResults;
using TheAggregate.Api.Models;
using TheAggregate.Shared.Util;

namespace TheAggregate.Api.Features.Feeds;

public interface IFeedsService
{
    Task<Result<List<Feed>>> GetFeedsAsync();
    Task<Result<Feed>> GetFeedByIdAsync(int id);
    Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl);
    Task<List<Result<Feed>>> UpdateFeedItemsFromSyndicationAsync(List<SyndicationFeed> feeds);
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
            return new List<Result<Feed>>(){Result.Fail<Feed>("Failed to get feeds")};
        }
        _logger.LogInformation($"[TheAggregate] Got: {feedsResult.Value.Count} feeds");


        var feeds = new List<Result<Feed>>();
        // var updateFeeds = new List<Feed>();
        foreach (var syndicationFeed in syndicationFeeds)
        {
            _logger.LogInformation($"[TheAggregate] Saving items from SyndicationFeed: {syndicationFeed.Links[0].Uri.ToString()}");
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
            foreach(var syndicationItem in syndicationFeed.Items)
            {
                var savedItem = feed.Items.FirstOrDefault(i =>
                    UrlUtil.NormalizeUrl(i.Url) == UrlUtil.NormalizeUrl(syndicationItem.Links[0].Uri.OriginalString));
                if (savedItem is null)
                {
                    var author = syndicationItem.Authors.FirstOrDefault()?.Name;
                    var summary = syndicationItem.Summary.Text;
                    var categories = syndicationItem.Categories.Select(c => c.Name).ToList();

                    var newItem = new FeedItem
                    {
                        Title = syndicationItem.Title.Text,
                        Author = author,
                        Url = syndicationItem.Links[0].Uri.OriginalString,
                        Summary = summary,
                        FeedId = feed.Id,
                        Published = syndicationItem.PublishDate.Date.ToUniversalTime(),
                        Categories = categories,
                    };
                    _logger.LogInformation($"[TheAggregate] Created FeedItem: {newItem.Title}");
                    feed.Items.Add(newItem);
                }
            }
            await _feedsRepository.UpdateFeedAsync(feed);
            feeds.Add(Result.Ok(feed)!);
        }

        return feeds;
    }

}