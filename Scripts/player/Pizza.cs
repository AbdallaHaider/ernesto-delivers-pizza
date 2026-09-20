using Godot;
using System;
using System.Diagnostics;

public partial class Pizza : Area2D
{
    [Signal]
    public delegate void HitEventHandler();


    double totalTime = 0.0f;
    private AnimatedSprite2D sprite;
    public Vector2 dir;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GD.Print("PIZZA!");
        Show();
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
    }

    public override void _PhysicsProcess(double delta)
    {
        totalTime += delta;
        if (totalTime > 0.1f)
        {
            totalTime = 0.0f;
            sprite.FlipH = !sprite.FlipH;
        }
        Position += (dir * (float)delta) * 200.0f;
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    } 
    private void OnBodyEntered(Area2D body)
    {
        GD.Print("succ");
        Hide();
        EmitSignal(SignalName.Hit);
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
}
