using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    // okay
}