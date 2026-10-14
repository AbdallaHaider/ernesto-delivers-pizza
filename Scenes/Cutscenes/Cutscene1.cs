using Godot;
using System;
using System.Threading.Tasks;

public partial class Cutscene1 : Node2D
{
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
			"what the hell man?? I was dreamin' of big money over there"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"If you were thinking of big money then you would've been out there",
			"delivering some pizzas right now!!!",
			"instead of sitting here and running my business to the ground!!!!!"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"aight chill out old man, if anything I'm the one keeping this place alive."
		]);

		await DialogueSystem.Instance.ShowMessage([
			"huh???????????????????????????????????????????",
			"just how delusional are you??",
			"...",
			"anyhow, we have some orders around town, how about you go work a little?"
		]);

		await DialogueSystem.Instance.ShowMessage([
			"man, it's not like I have much incentive anyways.",
			"I'd get paid more working at a retirement home...",
			"... I mean I guess I'm already doing that..."
		]);
		
		await DialogueSystem.Instance.ShowMessage([
			"I'm 60 years young!!! and you get paid in good pizza!!",
			"It's almost as good as getting paid in stocks."
		]);

		await DialogueSystem.Instance.ShowMessage([
			"tsk, don't start with your ramblings old basil. I'll go deliver some orders.",
			"say, that old recipe of yours that got this pizza joint so famous...",
			"what do you think of passing it to me? I'll do it justice, watch me."
		]);

		await DialogueSystem.Instance.ShowMessage([
			"That overconfidence of yours could get you killed in an instant. Stupid mushroom.",
			"Money and pizza aren't the only things you get paid with.",
			"I'm practically nurturing your being with all this knowledge and experience."
		]);

		await DialogueSystem.Instance.ShowMessage([
			"aight man save that for the department of labor when I report these worker rights violations.",
			"ima go catch some wind and deliver these babies."
		]);

		await DialogueSystem.Instance.ShowMessage([
			"HEY!!!"
		]);

		await Wait(1);
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
