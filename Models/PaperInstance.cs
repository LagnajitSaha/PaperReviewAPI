namespace PaperReviewAPI.Models;

public class PaperInstance
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string WorkflowName { get; set; }
    public string State { get; set; }
    public List<string> History { get; set; } = new();
    public bool Accepted => State == "accepted";
    public bool Rejected => State == "rejected";
}
