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
		sprite.Play();
	}

	public override void _Ready()
	{
		base._Ready();
		sprite = GetNode<AnimatedSprite2D>(ANIMATION_HANDLER_PATH);
		stateManager = GetParent().GetNode<StateManager>(STATE_MANAGER_PATH);
		stateManager.ChangeState += OnChangeState;

		sprite.Autoplay = "false";
		sprite.Play();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
