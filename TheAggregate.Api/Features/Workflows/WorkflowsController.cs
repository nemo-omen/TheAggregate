using Elsa.Workflows.Runtime;
using Microsoft.AspNetCore.Mvc;

namespace TheAggregate.Api.Features.Workflows;

[ApiController]
[Route("api/[controller]")]
public class WorkflowsController : Controller
{

    private readonly IWorkflowRegistry _workflowRegistry;

    public WorkflowsController(IWorkflowRegistry workflowRegistry)
    {
        _workflowRegistry = workflowRegistry;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}