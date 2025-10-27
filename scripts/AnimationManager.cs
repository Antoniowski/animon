using Godot;
using System;

[GodotClassName("AnimationManager")]
public partial class AnimationManager : Node
{
	// todo: change to animated sprite 2D
	private AnimatedSprite2D sprite;
	private StateManager stateManager;

	// CONSTANTS
	private const string ANIMATION_HANDLER_PATH = "../../Sprite";
	private const string STATE_MANAGER_PATH = "./StateManager";

	// SIGNALS
	[Signal]
	public delegate void AnimationFinishedEventHandler(string animationName);

    // CALLBACKS
    private void OnChangeState(string newState)
    {
        switch (newState)
        {
            case "BOOT":
                sprite.AnimationFinished += ExternalAnimationFinishedBoot;
                sprite.Play("boot");
                break;

            case "IDLE":
                sprite.SpriteFrames.SetAnimationLoop("idle", true);
                sprite.Play("idle");
                break;
        }
    }
    
    private void ExternalAnimationFinishedBoot()
    {
        EmitSignal(SignalName.AnimationFinished, "boot");
    }

	public override void _Ready()
	{
		base._Ready();
		sprite = GetNode<AnimatedSprite2D>(ANIMATION_HANDLER_PATH);
		stateManager = GetParent().GetNode<StateManager>(STATE_MANAGER_PATH);
		stateManager.ChangeState += OnChangeState;
		sprite.AnimationFinished += ExternalAnimationFinishedBoot;
		sprite.Play("boot");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
