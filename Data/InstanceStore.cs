using PaperReviewAPI.Models;

namespace PaperReviewAPI.Data;

public static class InstanceStore
{
    public static List<PaperInstance> Instances { get; set; } = new();
    public static int NextId = 1;
}
