namespace PaperReviewAPI.Models;
// Represents a single running instance of a workflow.
// Each instance is based on a defined workflow and progresses through states via actions.
public class PaperInstance
{
    // Unique identifier for the instance (auto-incremented).
    public int Id { get; set; }
    // Title of the paper associated with this workflow instance.
    public required string Title { get; set; }
    // Author of the paper (used for identification/display).
    public required string Author { get; set; }
    // Name of the workflow definition that this instance follows.
    public required string WorkflowName { get; set; }
    // Current state of this instance in the workflow lifecycle.
    public required string State { get; set; }
    // Ordered list of state names the instance has passed through.
    // Helps with tracking transitions and debugging.
    public List<string> History { get; set; } = new();
    public bool Accepted => State == "accepted";
    public bool Rejected => State == "rejected";
}
