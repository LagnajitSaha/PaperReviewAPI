using PaperReviewAPI.Models;

namespace PaperReviewAPI.Data
{
    // This static class acts as an in-memory store for all running paper instances.
    // It simulates persistence without a database, aligning with the assignment's requirement.
    public static class InstanceStore
    {
        // A globally accessible list to hold all active workflow instances (papers in progress).
        public static List<PaperInstance> Instances { get; } = new();
        // A simple auto-incrementing ID generator for new instances.
        // Starts at 1 and increments every time a new instance is created
        public static int NextId { get; set; } = 1;
    }
}
