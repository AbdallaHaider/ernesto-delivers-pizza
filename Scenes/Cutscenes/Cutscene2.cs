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

		await Wait(0.5);
		Ernesto.Show();
		OldMan.Show();

		await DialogueSystem.Instance.ShowMessage([
			"nice "
		]);

		await DialogueSystem.Instance.ShowMessage([
			"If you were thinking of big money,",
			"then you would've been out there",
			"delivering some pizzas!!!",
			"instead of sitting here",
			"and running my business to the ground"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"aight chill out old man,",
			"if anything I'm the one keeping",
			"this place alive."
		]);

		await DialogueSystem.Instance.ShowMessage([
			"huh?????",
			"just how delusional are you??",
			"...",
			"anyhow, we have some orders around town",
			"how about you go work a little?"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"before I go do that...",
			"what do you say about giving me",
			"the secret recipe to your pizzas?"
		]);
		
		await DialogueSystem.Instance.ShowMessage([
			"...",
			"that's family treasure kiddo,",
			"maybe I'll give it some thought",
			"if you go work a little."
		]);

		await Wait(2);
		GetTree().ChangeSceneToFile("res://Scenes/MAP.scn");
	}

	private async Task Wait(double seconds = 1.0f)
	{
		await ToSignal(GetTree().CreateTimer(seconds), SceneTreeTimer.SignalName.Timeout);
	}
	public override void _Process(double delta)
	{
	}
}
