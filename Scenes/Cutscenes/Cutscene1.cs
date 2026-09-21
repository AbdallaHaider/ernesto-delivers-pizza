using Godot;
using System;
using System.Threading.Tasks;

public partial class Cutscene1 : Node2D
{
	private bool cutsceneEnd = false;
	private Sprite2D Ernesto;
	private Sprite2D OldMan;
	private AnimatedSprite2D SLACKING;

	public override async void _Ready()
	{
		Ernesto = GetNode<Sprite2D>("Ernesto");
		OldMan = GetNode<Sprite2D>("OldMan");
		SLACKING = GetNode<AnimatedSprite2D>("SLACKER");

		await Wait(1);

		await DialogueSystem.Instance.ShowMessage([
			"OI!!!!!!!!!!!!!",
			"WAKE UP!!!!!!",
			"I DIDN'T HIRE YOU TO SLACK AROUND YOU MUSHROOM!!"
		]);

		await Wait(0.5);
		SLACKING.Hide();
		await Wait(0.5);
		Ernesto.Show();
		OldMan.Show();

		await DialogueSystem.Instance.ShowMessage([
			"what the hell man?? I was dreamin' of big money there"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"cu"
		]);
	}

	private async Task Wait(double seconds = 1.0f)
	{
		await ToSignal(GetTree().CreateTimer(seconds), SceneTreeTimer.SignalName.Timeout);
	}
	public override void _Process(double delta)
	{
	}
}
