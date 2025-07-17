using Microsoft.AspNetCore.Mvc;
using PaperReviewAPI.Data;
using PaperReviewAPI.Models;

namespace PaperReviewAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkflowsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateWorkflow([FromBody] Workflow workflow)
    {
        if (WorkflowStore.Workflows.ContainsKey(workflow.Name))
            return BadRequest("Workflow already exists");

        if (!workflow.States.Any(s => s.IsInitial))
            return BadRequest("No initial state found");

        WorkflowStore.Workflows[workflow.Name] = workflow;
        return Created("", workflow);
    }

    [HttpGet]
    public IActionResult GetAllWorkflows()
    {
        return Ok(WorkflowStore.Workflows.Keys);
    }

    [HttpGet("{name}")]
    public IActionResult GetWorkflow(string name)
    {
        if (!WorkflowStore.Workflows.TryGetValue(name, out var workflow))
            return NotFound("Workflow not found");
        return Ok(workflow);
    }
}
