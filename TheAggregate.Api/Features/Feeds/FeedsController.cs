using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAggregate.Api.Features.Feeds.GetFeed;
using TheAggregate.Api.Features.Feeds.GetFeedCategories;
using TheAggregate.Api.Features.Feeds.GetFeedItem;
using TheAggregate.Api.Features.Feeds.GetFeeds;
using TheAggregate.Api.Features.Feeds.Search;
using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;
using TheAggregate.Api.Shared.Types;

namespace TheAggregate.Api.Features.Feeds;

[ApiController]
[Route("api/[controller]")]
public class FeedsController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<FeedsController> _logger;

    public FeedsController(IMediator mediator, ILogger<FeedsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType<List<Feed>>(200)]
    [ProducesResponseType<ApiError>(500)]
    public async Task<ActionResult<List<Feed>>> GetFeeds()
    {
        var feedsResponse = await _mediator.Send(new GetFeedsCommand());
        if (feedsResponse.IsFailed)
        {
            return StatusCode(500, new ApiError
            {
                Type = "https://www.rfc-editor.org/rfc/rfc9110.html#name-500-internal-server-error",
                Title = "Internal Server Error",
                Status = 500,
                Detail = feedsResponse.Errors.First().Message
            });
        }
        return Ok(feedsResponse.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Feed>(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Feed>> GetFeed([FromRoute]Guid id)
    {
        var res = await _mediator.Send(new GetFeedQuery(id));
        if (res.IsFailed)
        {
            if(res.Errors.Any(e => e.Message == "Not found")) return NotFound();
            return StatusCode(500, res.Errors);
        }
        return Ok(res.Value);
    }

    [HttpGet("categories")]
    [ProducesResponseType<List<FeedCategoryResponse>>(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetFeedCategoriesAsync()
    {
        var categoriesResult = await _mediator.Send(new GetFeedCategoriesCommand());
        if (categoriesResult.IsFailed)
        {
            return StatusCode(500, categoriesResult.Errors);
        }
        return Ok(categoriesResult.Value);
    }


    [HttpPost("search")]
    [ProducesResponseType<SearchResponse>(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<SearchResponse>> Search(string searchTerm)
    {
        var res = await _mediator.Send(new SearchQuery(searchTerm));
        if(res.IsFailed) return StatusCode(500, res.Errors);
        return Ok(res.Value);
    }

    [HttpGet("/FeedItems/{id}")]
    [ProducesResponseType<FeedItem>(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<FeedItem>> GetFeedItem(Guid id)
    {
        var res = await _mediator.Send(new GetFeedItemQuery(id));
        if (res.IsFailed)
        {
            if(res.Errors.Any(e => e.Message == "Not found")) return NotFound();
            return StatusCode(500, res.Errors);
        }
        return Ok(res.Value);
    }
}