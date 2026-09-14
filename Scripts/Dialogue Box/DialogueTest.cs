using Godot;
using System;

public partial class DialogueTest : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

    public override void _Process(double delta)
    {
        		if (Input.IsKeyPressed(Key.P) &&
			Input.IsKeyPressed(Key.I) &&
			Input.IsKeyPressed(Key.Z) &&
			Input.IsKeyPressed(Key.A))
		{
			GetNode<DialogueSystem>("Control").ShowMessage("stop slacking and deliver some god damn pizzas man.");	
		}
    }

}
