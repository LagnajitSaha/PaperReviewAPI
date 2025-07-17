namespace PaperReviewAPI.Models;

public class Workflow
{
    public required string Name { get; set; }
    public required List<State> States { get; set; }
    public required List<ActionDef> Actions { get; set; }
}

public class State
{
    public required string Name { get; set; }
    public bool IsInitial { get; set; }
}

public class ActionDef
{
    public required string Name { get; set; }
    public required List<string> FromStates { get; set; }
    public required string ToState { get; set; }
    public bool Enabled { get; set; } = true;
}
