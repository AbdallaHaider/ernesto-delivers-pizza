using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{

    [Export]
    private PackedScene Pizza { get; set; }

    [Export]
    public int Speed { get; set; } = 125;

    public Vector2 ScreenSize;

    public AnimatedSprite2D sprite;

    public Vector2 yAxis = new Vector2(0.0f, 1.0f);

    public Vector2 xAxis = new Vector2(1.0f, 0.0f);

    public Vector2 velocity = Vector2.Zero;

    double totalTime = 0.0f;

    StringName[] animList = { "up_idle", "sideways_idle", "down_idle", "up_moving", "sideways_moving", "down_moving" };

    public Vector2 fireDir = Vector2.Zero;

    bool shouldFire = false;

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
        sprite.Animation = animList[index];
        sprite.SpeedScale = 1.25f;
        sprite.Play();
    }

    public override void _Process(double delta)
    {
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

        if (Input.IsActionJustPressed("fire_pizza"))
        {
            shouldFire = true;
        }

    }

    public void FirePizzaGun(Vector2 FireDir)
    {
        Pizza pizzaProjectile = Pizza.Instantiate<Pizza>();
        pizzaProjectile.dir.X = FireDir.X;
        pizzaProjectile.dir.Y = FireDir.Y;
        GD.Print(pizzaProjectile.dir.X + "," + pizzaProjectile.dir.Y);
        pizzaProjectile.Position = Position;
        GetTree().Root.AddChild(pizzaProjectile);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        totalTime += delta;
        if (velocity.Length() > 0)
        {
            sprite.SpeedScale = 2.0f;
            Vector2 DirVelocity = velocity.Normalized();
            velocity = DirVelocity * (float)Speed * (float)delta;
            float horDir = ((float)Math.Round(xAxis.Dot(DirVelocity)));
            float absDir = Math.Abs(horDir);
            float down = ((float)Math.Round((DirVelocity.Y * 0.5f + 0.5f)));
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

            MoveAndCollide(velocity * (float)delta * Speed);

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


        velocity = Vector2.Zero;

    }
}
