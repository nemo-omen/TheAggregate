using System.ServiceModel.Syndication;
using FluentResults;
using TheAggregate.Api.Models;
using TheAggregate.Shared.Infrastructure;

namespace TheAggregate.Api.Features.SyndicationFeeds;

public interface IAggregationService
{
    Task<List<Result<SyndicationFeed>>> GetSyndicationFeedsFromFeedsAsync(List<Feed> feeds);
    Task<Result<SyndicationFeed>> GetSyndicationFeedFromFeedAsync(Feed feed);
}

// Note: I need to really determine what I want the behavior
// of AggregationService to be. I know I'm getting a list of Result<SyndicationFeed>
// from GetFeedsFromNewsSourcesAsync, but sometimes they can be failures, so there's
// some additional thought that needs to be put into this.
public class AggregationService : IAggregationService
{
    private readonly IFeedReader _feedReader;
    
    public AggregationService()
    {
        _feedReader = new FeedReader(
            new HttpClient(),
            RetryPipelineFactory.CreateExponentialBackoffPipeline());
    }

    /// <summary>
    /// Retrieves the syndication feed results from a list of news sources asynchronously.
    /// </summary>
    /// <param name="feeds">The list of news sources to retrieve feeds from.</param>
    /// <returns>A list of results containing the syndication feed or an error message.</returns>
    public async Task<List<Result<SyndicationFeed>>> GetSyndicationFeedsFromFeedsAsync(List<Feed> feeds)
    {
        var feedResults = new List<Result<SyndicationFeed>>();
        const int batchSize = 10;
        foreach (var batch in feeds.Chunk(batchSize))
        {
            var tasks = batch.Select(async newsSource => await GetSyndicationFeedFromFeedAsync(newsSource));
            feedResults.AddRange(await Task.WhenAll(tasks));
        }
        return feedResults;
    }
    
    /// <summary>
    /// Retrieves the syndication feed result for a given news source asynchronously.
    /// </summary>
    /// <param name="feed">The news source to retrieve the feed from.</param>
    /// <returns>A result containing the syndication feed or an error message.</returns>
    public async Task<Result<SyndicationFeed>> GetSyndicationFeedFromFeedAsync(Feed feed)
    {
        var feedResult = await _feedReader.ReadAsync(feed.FeedUrl);
        return feedResult;
    }
}