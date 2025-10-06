using Godot;
using System;

public enum States
{
    BOOT,
    IDLE,
    TIRED,
    SLEEP,
    ANGRY,
    PLAYFUL
}

[GodotClassName("StateManager")]
public partial class StateManager : Node
{
    private States currentState;
    private AnimationManager animationManager;
    private Timer timer;

    // CONSTANTS
    private const string ANIMATION_MANAGER_PATH = "../AnimationManager";

    // SIGNALS
    [Signal]
    public delegate void ChangeStateEventHandler(string newState);

    // TIMER FUNCTIONS
    public void OnTimerTimeout()
    {
        timer.Stop();
        ResetTimer();
        currentState = States.ANGRY;
        EmitSignal(SignalName.ChangeState, currentState.ToString());
    }

    public void ResetTimer()
    {
        if (timer.IsStopped())
        {
            timer.WaitTime = new RandomNumberGenerator().RandfRange(10, 30);
        }
    }

    // CALLBACKS
    private void OnAnimationFinished(string fineshedAnimName)
    {
        
    }

    public override void _Ready()
    {
        timer = GetParent().GetNode<Timer>("Timer");
        animationManager = GetNode<AnimationManager>(ANIMATION_MANAGER_PATH);
        animationManager.AnimationFinished += OnAnimationFinished;

        currentState = States.BOOT;
        // ADD BOOT ANIMATION SIGNAL

        // TIMER FIRST SETUP
        timer.WaitTime = new RandomNumberGenerator().RandfRange(10, 30);
        timer.Timeout += OnTimerTimeout;
        timer.Start();
    }

    public override void _Process(double delta)
    {

    }
}
