namespace PaperReviewAPI.Models;

/// <summary>
/// Represents a full workflow definition that clients can configure.
/// Contains a set of states and actions (transitions) that define a state machine.
/// </summary>
public class Workflow
{
    /// <summary>
    /// Unique name of the workflow (e.g., "paper-review").
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// List of all possible states for this workflow.
    /// Must contain exactly one state marked as IsInitial.
    /// </summary>
    public required List<State> States { get; set; }
    /// <summary>
    /// List of actions (transitions) allowed between states in this workflow.
    /// </summary>
    public required List<ActionDef> Actions { get; set; }
}
/// <summary>
/// Represents a single state within a workflow.
/// </summary>
public class State
{
    /// <summary>
    /// Name of the state (e.g., "submitted", "approved").
    /// Must be unique within a workflow.
    /// </summary
    public required string Name { get; set; }
    /// <summary>
    /// Indicates whether this is the initial state where new instances should begin.
    /// </summary>
    public bool IsInitial { get; set; }
}

/// <summary>
/// Represents an action/transition that can move an instance from one or more states to another.
/// </summary>

public class ActionDef
{
    /// <summary>
    /// Unique name of the action (e.g., "approve", "reject").
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// List of state names from which this action can be executed.
    /// </summary>
    public required List<string> FromStates { get; set; }
    /// <summary>
    /// The target state that the instance moves to when this action is executed.
    /// </summary>
    public required string ToState { get; set; }
    /// <summary>
    /// Determines whether the action is currently enabled.
    /// </summary>
    public bool Enabled { get; set; } = true;
}
