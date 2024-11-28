using FluentResults;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Feeds;

public interface IFeedsService
{
    Task<Result<List<Feed>>> GetFeedsAsync();
    Task<Result<Feed>> GetFeedByIdAsync(int id);
    Task<Result<Feed>> GetFeedByFeedUrlAsync(string feedUrl);
    // Task<Result<Feed>> CreateFeedAsync(Feed feed);
    // Task<Result<Feed>> UpdateFeedAsync(Feed feed);
    // Task<Result<Feed>> DeleteFeedAsync(int id);
}

public class FeedsService : IFeedsService
{
    private readonly IFeedsRepository _feedsRepository;
    
    public FeedsService(IFeedsRepository feedsRepository)
    {
        _feedsRepository = feedsRepository;
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
}