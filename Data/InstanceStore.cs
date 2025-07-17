using PaperReviewAPI.Models;

namespace PaperReviewAPI.Data
{
    public static class InstanceStore
    {
        public static List<PaperInstance> Instances { get; } = new();
        public static int NextId { get; set; } = 1;
    }
}
