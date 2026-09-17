using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : Area2D
{

    [Export]
    public int Speed { get; set; } = 1;

	public Vector2 ScreenSize;

	public AnimatedSprite2D sprite;

	public Vector2 yAxis = new Vector2(0.0f, 1.0f);

    public Vector2 xAxis = new Vector2(1.0f, 0.0f);

    public Vector2 velocity = Vector2.Zero;

    public bool prevDir = true;

    public bool stoppedMoving = true;

    StringName[] animList = { "moped", "moped_sideways" };


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GD.Print("venus");
		Show();
		ScreenSize = GetViewportRect().Size;
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("move_right"))
        {
            stoppedMoving = false;
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            stoppedMoving = false;
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            stoppedMoving = false;
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            stoppedMoving = false;
            velocity.Y -= 1;
        }


    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            var shouldFlip = (Math.Sign(xAxis.Dot(velocity)) * 0.5f + 0.5f);
            int upOrDown = (1 - Math.Abs((int)Math.Round(yAxis.Dot(velocity))));
            sprite.FlipH = (shouldFlip < 1.0);
            sprite.Animation = animList[upOrDown];
            sprite.Play();
        }

        Position += velocity;

        //choose between up or left


        velocity = Vector2.Zero;
        stoppedMoving = true;

    }
}
