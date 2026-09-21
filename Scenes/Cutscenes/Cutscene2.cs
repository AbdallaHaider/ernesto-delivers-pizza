using Godot;
using System;

public partial class Cutscene2 : Node2D
{
	private Sprite2D Ernesto;
	private Sprite2D OldMan;
	private AnimatedSprite2D SLACKING;

	public override async void _Ready()
	{
		Ernesto = GetNode<Sprite2D>("Ernesto");
		OldMan = GetNode<Sprite2D>("OldMan");

		Ernesto.Show();
		OldMan.Show();

		await DialogueSystem.Instance.ShowMessage([
			"nice job there,"
		]);
		GetTree().ChangeSceneToFile("res://Scenes/MAP.scn");
	}

	public override void _Process(double delta)
	{
	}
}
