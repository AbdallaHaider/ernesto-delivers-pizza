using Godot;
using System;
using System.Threading.Tasks;

public partial class Cutscene1 : Node2D
{
	private bool cutsceneEnd = false;
	private Sprite2D Ernesto;
	private Sprite2D OldMan;

	public override async void _Ready()
	{
		Ernesto = GetNode<Sprite2D>("Ernesto");
		OldMan = GetNode<Sprite2D>("OldMan");


		await DialogueSystem.Instance.ShowMessage([
			"sup cuh"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"cu"
		]);
	}

	private async Task Wait(float seconds = 1.0f)
	{
		await ToSignal(GetTree().CreateTimer(seconds), SceneTreeTimer.SignalName.Timeout);
	}
	public override void _Process(double delta)
	{
	}
}
