using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType<List<Subscription>>(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<List<Subscription>>> GetSubscriptions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }
        var subscriptions = await _subscriptionsService.GetSubscriptions(userId);
        return Ok(subscriptions);
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