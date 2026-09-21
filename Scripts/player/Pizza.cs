using Godot;
using System;
using System.Diagnostics;

public partial class Pizza : Area2D
{
    [Signal]
    public delegate void HitEventHandler();


    private AnimatedSprite2D sprite;
    public Vector2 dir;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        sprite.Play();
        GD.Print("PIZZA!");
        Show();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += (dir * (float)delta) * 250.0f;
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    } 
    private void OnBodyEntered(Area2D body)
    {
        House hitHouse = body.GetParent() as House;
        if (hitHouse != null && hitHouse.IsWaitingForPizza)
        {
        hitHouse.ReceivePizza();
        GD.Print("succ");
        Hide();
        EmitSignal(SignalName.Hit);
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        QueueFree();
        }
    }
}
