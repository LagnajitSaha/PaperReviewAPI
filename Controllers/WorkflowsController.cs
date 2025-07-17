using Microsoft.AspNetCore.Mvc;
using PaperReviewAPI.Data;
using PaperReviewAPI.Models;

namespace PaperReviewAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkflowsController : ControllerBase
{
    // Endpoint to create a new workflow definition (states + actions)
    // Validates uniqueness and presence of one initial state
    [HttpPost]
    public IActionResult CreateWorkflow([FromBody] Workflow workflow)
    {
        // Ensure workflow name is unique
        if (WorkflowStore.Workflows.ContainsKey(workflow.Name))
            return BadRequest("Workflow already exists");
        // Validate: at least one state should be marked as initial
        if (!workflow.States.Any(s => s.IsInitial))
            return BadRequest("No initial state found");

        WorkflowStore.Workflows[workflow.Name] = workflow;
        return Created("", workflow);
    }
    // Endpoint to list all workflow names currently defined
    [HttpGet]
    public IActionResult GetAllWorkflows()
    {
        return Ok(WorkflowStore.Workflows.Keys);
    }
    // Endpoint to retrieve a full workflow definition by its name
    [HttpGet("{name}")]
    public IActionResult GetWorkflow(string name)
    {
        if (!WorkflowStore.Workflows.TryGetValue(name, out var workflow))
            return NotFound("Workflow not found");
        return Ok(workflow);
    }
}
