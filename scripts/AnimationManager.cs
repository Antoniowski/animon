using Godot;
using System;

[GodotClassName("AnimationManager")]
public partial class AnimationManager : Node
{
    // todo: change to animated sprite 2D
    private RichTextLabel text;
    private StateManager stateManager;

    // CONSTANTS
    private const string ANIMATION_HANDLER_PATH = "../../RichTextLabel";
    private const string STATE_MANAGER_PATH = "./StateManager";

    // SIGNALS
    [Signal]
    public delegate void AnimationFinishedEventHandler(string animationName);

    // CALLBACKS
    private void OnChangeState(string newState)
    {
        text.Text = newState;
    }

    public override void _Ready()
    {
        base._Ready();
        text = GetNode<RichTextLabel>(ANIMATION_HANDLER_PATH);
        stateManager = GetParent().GetNode<StateManager>(STATE_MANAGER_PATH);
        stateManager.ChangeState += OnChangeState;

        text.Text = "IDLE";
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}
