using PaperReviewAPI.Models;

namespace PaperReviewAPI.Data;

public static class WorkflowStore
{
    public static Dictionary<string, Workflow> Workflows { get; set; } = new();
}
