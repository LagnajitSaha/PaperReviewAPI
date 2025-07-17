namespace PaperReviewAPI.Models;

public class Workflow
{
    public string Name { get; set; }
    public List<State> States { get; set; }
    public List<ActionDef> Actions { get; set; }
}

public class State
{
    public string Name { get; set; }
    public bool IsInitial { get; set; }
}

public class ActionDef
{
    public string Name { get; set; }
    public List<string> FromStates { get; set; }
    public string ToState { get; set; }
    public bool Enabled { get; set; } = true;
}
