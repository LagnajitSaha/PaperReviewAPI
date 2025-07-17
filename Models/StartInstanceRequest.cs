namespace PaperReviewAPI.Models;

public class StartInstanceRequest
{
    public string WorkflowName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}
