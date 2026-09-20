using Godot;
using System;

public partial class PizzaHitbox : CollisionShape2D
{

}

public partial class House : StaticBody2D
{
    [Export]
    private PackedScene Window { get; set; }
    
	int houseIndex = 0;
	bool isDeliverable = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void SetDeliverable()
	{
		isDeliverable = true;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
