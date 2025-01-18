using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAggregate.Api.Features.Subscriptions.Types;
using TheAggregate.Api.Models;

namespace TheAggregate.Api.Features.Subscriptions;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : Controller
{
    private readonly ISubscriptionsService _subscriptionsService;

    public SubscriptionsController(ISubscriptionsService subscriptionsService)
    {
        _subscriptionsService = subscriptionsService;
    }

    [HttpGet]
    [ProducesResponseType<List<SubscriptionFeedResponse>>(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<List<SubscriptionFeedResponse>>> GetSubscriptions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }
        var subscriptions = await _subscriptionsService.GetSubscriptions(userId);
        return Ok(subscriptions);
    }

    [HttpGet("items")]
    [ProducesResponseType<List<SubscriptionFeedItem>>(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<List<SubscriptionFeedItem>>> GetSubscriptionItems()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }
        var subscriptionItems = await _subscriptionsService
            .GetSubscriptionItems(userId);

        return Ok(subscriptionItems);
    }

    [HttpPost("{feedId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> SubscribeToFeed([FromRoute]Guid feedId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }
        await _subscriptionsService.SubscribeToFeed(userId, feedId);
        return Ok();
    }
}