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

    public Vector2 velocity = Vector2.Zero;


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
        velocity = Vector2.Zero;
        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
	
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			sprite.Play();
		}

        Position += velocity;
			
		//choose between up or left
	}
}
