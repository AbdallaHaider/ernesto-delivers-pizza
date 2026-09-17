using Godot;
using System;

public partial class DialogueTest : CanvasLayer
{
	[Export] private PackedScene _SLACKING;
	private bool hasSlacked = false;

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.P) &&
			Input.IsKeyPressed(Key.I) &&
			Input.IsKeyPressed(Key.Z) &&
			Input.IsKeyPressed(Key.A) &&
			!hasSlacked)
		{
			Node2D SlackingInstance = _SLACKING.Instantiate<Node2D>();
			GetTree().CurrentScene.AddChild(SlackingInstance);

			DialogueSystem.Instance.ShowMessage(
            [
                "stop slacking and deliver some god damn pizzas man.",
				"aloooo, acorda filho da puta",
				"porra"
			]);
			hasSlacked = true;
		}
    }

}
