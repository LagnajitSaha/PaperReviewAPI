using Microsoft.AspNetCore.Mvc;
using PaperReviewAPI.Data;
using PaperReviewAPI.Models;

namespace PaperReviewAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InstancesController : ControllerBase
{
    // Starts a new workflow instance for a given workflow name
    // Validates workflow existence and ensures it has a defined initial state
    [HttpPost]
    public IActionResult StartInstance([FromBody] StartInstanceRequest req)
    {
        // Check if the workflow exists
        if (!WorkflowStore.Workflows.TryGetValue(req.WorkflowName, out var workflow))
            return NotFound("Workflow not found");
        // Find the initial state in the workflow (should be exactly one)
        var initial = workflow.States.FirstOrDefault(s => s.IsInitial);
        if (initial == null)
            return BadRequest("No initial state found");

        // Initialize a new paper instance starting at the initial state
        var instance = new PaperInstance
        {
            Id = InstanceStore.NextId++,
            Title = req.Title,
            Author = req.Author,
            WorkflowName = req.WorkflowName,
            State = initial.Name,
            History = new List<string> { initial.Name }
        };
        // Save instance in memory
        InstanceStore.Instances.Add(instance);
        return Created("", instance);
    }

    [HttpGet]
    public IActionResult GetAllInstances()
    {
        return Ok(InstanceStore.Instances);
    }
    // Gets the details of a specific workflow instance by its unique ID
    [HttpGet("{id}")]
    public IActionResult GetInstance(int id)
    {
        var inst = InstanceStore.Instances.FirstOrDefault(i => i.Id == id);
        if (inst == null) return NotFound("Instance not found");
        return Ok(inst);
    }
    // Executes a valid action on a given instance to transition it to the next state
    [HttpPost("execute")]
    public IActionResult ExecuteAction([FromBody] ExecuteRequest request)
    {
        var instance = InstanceStore.Instances.FirstOrDefault(i => i.Id == request.Id);
        if (instance == null) return NotFound("Instance not found");

        if (!WorkflowStore.Workflows.TryGetValue(instance.WorkflowName, out var workflow))
            return NotFound("Workflow not found");

        var action = workflow.Actions.FirstOrDefault(a =>
            a.Name == request.ActionName && a.Enabled && a.FromStates.Contains(instance.State));

        if (action == null)
            return BadRequest($"Invalid or disallowed action '{request.ActionName}' from state '{instance.State}'");
        // Perform state transition
        instance.State = action.ToState;
        instance.History.Add(action.ToState);

        return Ok(instance);
    }
    // DTO for action execution
    // Used to trigger a transition on a specific instanc
    public class ExecuteRequest
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
    }
}
