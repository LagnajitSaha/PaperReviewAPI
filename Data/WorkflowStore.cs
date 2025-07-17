using PaperReviewAPI.Models;

namespace PaperReviewAPI.Data
{
    // Static class responsible for storing all defined workflow configurations in-memory.
    // This satisfies the assignment's requirement for lightweight persistence without a database.
    public static class WorkflowStore
    {
        // Dictionary to hold workflow definitions, where the key is the workflow's unique name.
        // This allows efficient lookup, creation, and retrieval of workflow structures.
        public static Dictionary<string, Workflow> Workflows { get; } = new();
    }
}
