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

    double time_since_last_shot = 0.0f;

    double time_since_invincibility = 0.0f;

    double time_since_stagger = 0.0;

    StringName[] animList = { "up_idle", "sideways_idle", "down_idle", "up_moving", "sideways_moving", "down_moving" };

    public Vector2 fireDir = Vector2.Zero;

    bool shouldFire = false;

    bool invincible = false;

    float pressed = 0;

    float accel = 1;

    int index = 1;

    int idle_ind = 1;

    public enum States
    {
        MOVING,
        STAGGERED
    }

    private States State;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        DeliveryManager.Instance.StartDeliveryRound(30);
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

    public void move(double delta)
    {
        time_since_last_shot += delta;
        if (invincible)
        {
            time_since_invincibility += delta;
            if ((int)Math.Round((time_since_invincibility / 3.0) * 40.0) % 2.0 == 0)
            {
                sprite.Hide();
            }
            else
            {
                sprite.Show();
            }
            if (time_since_invincibility > 3.0f)
            {
                time_since_invincibility = 0;
                sprite.Show();
                invincible = false;
            }
        }

        Speed += 1.0f * accel * (float)delta;

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


        if (shouldFire)
        {
            if (time_since_last_shot > 0.25)
            {
                GD.Print("GO PIZZA!");
                FirePizzaGun(fireDir);
                time_since_last_shot = 0.0;
            }
            shouldFire = false;
        }


    }

    public void Stagger(double delta)
    {
        Speed -= 0.1f;
        pressed = 0.0f;
        time_since_stagger += delta;
        index = (int)Math.Floor(time_since_stagger * 9) % 3;
        sprite.Animation = animList[index];
        sprite.FlipH = ((int)Math.Floor(time_since_stagger * 3) % 3 == 0);
        velocity = (prevDir * Speed);
        if (time_since_stagger > 1.0)
        {
            time_since_stagger = 0;
            GD.Print("recovered!");
            prevDir.X = -prevDir.X;
            prevDir.X = -prevDir.X;
            invincible = true;
            State = States.MOVING;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{

        var getCollision = MoveAndCollide(velocity);

        if (getCollision != null && !invincible)
        {
            if (Speed > 2.0)
            {
                GD.Print("Ouch!");
                index = 0;
                prevDir.X = -prevDir.X;
                prevDir.Y = -prevDir.Y;
                State = States.STAGGERED;
                ScoreManager.Instance.AddScore(-100);
                ScoreManager.Instance.ResetMult();
            }
        }

        switch (State)
        {
            case States.MOVING:
                move((float)delta);
                break;
            case States.STAGGERED:

                Stagger((float)delta);
                break;

        }

        Speed = Math.Clamp(Speed, 0.0f, 3.5f);
        Dir = Vector2.Zero;
        pressed = 0.0f;

    }
}
