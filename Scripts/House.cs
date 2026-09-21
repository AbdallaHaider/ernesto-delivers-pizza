using Godot;
using System;

public partial class PizzaHitbox : CollisionShape2D
{

}

public partial class House : StaticBody2D
{
    [Export]
    private PackedScene Window { get; set; }

	[Export] public Sprite2D HouseSprite;
    [Export] public CollisionShape2D WindowCollision;
    
	public bool IsWaitingForPizza { get; private set; } = false;

	int houseIndex = 0;
	bool isDeliverable = false;

	public void OpenWindow()
    {
        IsWaitingForPizza = true;
        
        // Swap the texture file name automatically
        string currentPath = HouseSprite.Texture.ResourcePath;
        string newPath = currentPath.Replace("_closed", "_open");
        HouseSprite.Texture = GD.Load<Texture2D>(newPath);

        WindowCollision.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
    }

	public void ReceivePizza()
    {
        if (!IsWaitingForPizza) return;
        IsWaitingForPizza = false;

        // Turn off the collision box
        WindowCollision.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

        // Change the sprite back
        string currentPath = HouseSprite.Texture.ResourcePath;
        string newPath = currentPath.Replace("_open", "_closed");
        HouseSprite.Texture = GD.Load<Texture2D>(newPath);

        // Tell the manager to subtract 1
        DeliveryManager.Instance.HouseDelivered();
    }

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
