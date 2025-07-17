namespace PaperReviewAPI.Models;

/// <summary>
/// DTO (Data Transfer Object) used to capture the request payload 
/// when starting a new workflow instance.
/// </summary>
public class StartInstanceRequest
{
    /// <summary>
    /// The name of the workflow this instance should follow.
    /// Must match a pre-configured workflow definition.
    /// </summary>
    public string WorkflowName { get; set; } = null!;
    /// <summary>
    /// Title of the paper or subject being submitted into the workflow.
    /// </summary>
    public string Title { get; set; } = null!;
    /// <summary>
    /// Name of the author initiating the workflow instance.
    /// </summary>
    public string Author { get; set; } = null!;
}
