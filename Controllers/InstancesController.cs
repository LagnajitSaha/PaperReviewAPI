using Microsoft.AspNetCore.Mvc;
using PaperReviewAPI.Data;
using PaperReviewAPI.Models;

namespace PaperReviewAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InstancesController : ControllerBase
{
    [HttpPost]
    public IActionResult StartInstance([FromBody] StartInstanceRequest req)
    {
        if (!WorkflowStore.Workflows.TryGetValue(req.WorkflowName, out var workflow))
            return NotFound("Workflow not found");

        var initial = workflow.States.FirstOrDefault(s => s.IsInitial);
        if (initial == null)
            return BadRequest("No initial state found");

        var instance = new PaperInstance
        {
            Id = InstanceStore.NextId++,
            Title = req.Title,
            Author = req.Author,
            WorkflowName = req.WorkflowName,
            State = initial.Name,
            History = new List<string> { initial.Name }
        };

        InstanceStore.Instances.Add(instance);
        return Created("", instance);
    }

    [HttpGet]
    public IActionResult GetAllInstances()
    {
        return Ok(InstanceStore.Instances);
    }

    [HttpGet("{id}")]
    public IActionResult GetInstance(int id)
    {
        var inst = InstanceStore.Instances.FirstOrDefault(i => i.Id == id);
        if (inst == null) return NotFound("Instance not found");
        return Ok(inst);
    }

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

        instance.State = action.ToState;
        instance.History.Add(action.ToState);

        return Ok(instance);
    }

    public class ExecuteRequest
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
    }
}
