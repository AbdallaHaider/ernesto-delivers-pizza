using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{

    [Export]
    private PackedScene Pizza { get; set; }

    [Export]
    public float Speed { get; set; } = 0;

    public Vector2 ScreenSize;

    public AnimatedSprite2D sprite;


    public Vector2 yAxis = new Vector2(0.0f, 1.0f);

    public Vector2 xAxis = new Vector2(1.0f, 0.0f);

    public Vector2 velocity = Vector2.Zero;

    public Vector2 Dir = Vector2.Zero;

    public Vector2 prevDir = Vector2.Zero;

    double totalTime = 0.0f;

    double time = 0.0;

    StringName[] animList = { "up_idle", "sideways_idle", "down_idle", "up_moving", "sideways_moving", "down_moving" };

    public Vector2 fireDir = Vector2.Zero;

    bool shouldFire = false;

    float pressed = 0;

    float accel = 1;

    int index = 1;

    int idle_ind = 1;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GD.Print("venus");
		Show();
		ScreenSize = GetViewportRect().Size;
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        fireDir.X = 0.0f;
        fireDir.Y = -1.0f;
        prevDir.X = 1.0f;
        prevDir.Y = 0.0f;
        sprite.Animation = animList[index];
        sprite.SpeedScale = 1.25f;
        sprite.Play();
    }

    public override void _Process(double delta)
    {
        accel = 2; 

        if (Input.IsActionPressed("move_right"))
        {
            Dir.X = 1;
            pressed = 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            Dir.X = -1;
            pressed = 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            Dir.Y = 1;
            pressed = 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            Dir.Y = -1;
            pressed = 1;
        }

        if (Input.IsActionJustPressed("fire_pizza"))
        {
            shouldFire = true;
        }

        if (Input.IsActionPressed("deAccel"))
        {
            accel = -8.0f;
        }

        Dir = Dir.Normalized();

    }

    public void FirePizzaGun(Vector2 FireDir)
    {
        Pizza pizzaProjectile = Pizza.Instantiate<Pizza>();
        pizzaProjectile.dir.X = FireDir.X;
        pizzaProjectile.dir.Y = FireDir.Y;
        GD.Print(pizzaProjectile.dir.X + "," + pizzaProjectile.dir.Y);
        //pizzaProjectile.Position = collisionPos.GlobalPosition;
        pizzaProjectile.Position = new Vector2(Position.X, Position.Y - 8);
        GetTree().Root.AddChild(pizzaProjectile);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        totalTime += delta;

        Speed += 1.0f * accel * (float)delta;
        Speed = Math.Clamp(Speed, 0.0f, 3.5f);

        if (Speed > 0)
        {
            sprite.SpeedScale = 2.0f;
            float horDir = ((float)Math.Round(xAxis.Dot(prevDir)));
            float absDir = Math.Abs(horDir);
            float down = ((float)Math.Round((prevDir.Y * 0.5f + 0.5f)));
            sprite.FlipH = (horDir < 0.0);
            index = (int)absDir + 2 * (int)(down) * (1 - (int)absDir) + 3;
            sprite.Animation = animList[index];
            fireDir.X = -1.0f + absDir;
            fireDir.Y = -absDir;
            sprite.Play();
        } 
        else
        {
            if (idle_ind != index)
            {
                sprite.SpeedScale = 1.25f;
                index -= 3;
                idle_ind = index;
                sprite.Animation = animList[index];
            }
        }
        time += delta;
        if (time > 2.0)
        {
            GD.Print(Dir);
            GD.Print(velocity);
            GD.Print(Speed);
            time = 0;
        }

        if (pressed == 1)
        {
            velocity = Dir * Speed;
            prevDir.X = Dir.X;
            prevDir.Y = Dir.Y;
        } 
        else
        {
            velocity = prevDir * Speed;
        }

            MoveAndCollide(velocity);

        if (shouldFire)
        {
            if (totalTime > 0.25)
            {
                GD.Print("GO PIZZA!");
                FirePizzaGun(fireDir);
                totalTime = 0.0;
            }
            shouldFire = false;
        }
        //choose between up or left

        Dir = Vector2.Zero;
        pressed = 0.0f;

    }
}
