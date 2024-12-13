using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAggregate.Api.Features.Feeds.GetFeeds;
using TheAggregate.Api.Features.Feeds.Search;
using TheAggregate.Api.Features.Feeds.Types;
using TheAggregate.Api.Models;

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
    public async Task<ActionResult<List<Feed>>> GetFeeds()
    {
        var feedsResponse = await _mediator.Send(new GetFeedsCommand());
        if (feedsResponse.IsFailed)
        {
            return StatusCode(500, feedsResponse.Errors);
        }
        return Ok(feedsResponse.Value);
    }

    [HttpPost("search")]
    [ProducesResponseType<SearchResponse>(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<FeedItem>>> Search(string searchTerm)
    {
        var res = await _mediator.Send(new SearchQuery(searchTerm));
        if(res.IsFailed) return StatusCode(500, res.Errors);
        return Ok(res.Value);
    }
}