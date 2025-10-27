using Godot;
using System;

public enum States
{
    BOOT,
    IDLE,
    TIRED,
    SLEEP,
    ANGRY,
    PLAYFUL,
    DISTRACTED,
    MEME //TO BE DEFINED
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
        currentState = States.IDLE;
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
        switch(fineshedAnimName)
        {
            case "boot":
                // TIMER FIRST SETUP
                timer.WaitTime = new RandomNumberGenerator().RandfRange(5, 7);
                timer.Timeout += OnTimerTimeout;
                timer.Start();
                break;
        }
    }

    public override void _Ready()
    {
        timer = GetParent().GetNode<Timer>("Timer");
        animationManager = GetNode<AnimationManager>(ANIMATION_MANAGER_PATH);
        animationManager.AnimationFinished += OnAnimationFinished;

        currentState = States.BOOT;
        EmitSignal(SignalName.ChangeState, currentState.ToString());
        // ADD BOOT ANIMATION SIGNAL
    }

    public override void _Process(double delta)
    {

    }
}
