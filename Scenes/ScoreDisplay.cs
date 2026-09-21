using Godot;
using System;

public partial class ScoreDisplay : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = "Score: " + ScoreManager.Instance.CurrentScore + " " + ScoreManager.Instance.CurrentMultiplier + "x";

        ScoreManager.Instance.ScoreChanged += OnScoreChanged;
	}

	private void OnScoreChanged(int newScore, int newMult)
    {
        Text = "Score: " + newScore + " " + newMult + "x";
    }

	public override void _ExitTree()
    {
        // Always unsubscribe from global signals when the node is destroyed to prevent errors
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ScoreChanged -= OnScoreChanged;
        }
	}
	public override void _Process(double delta)
	{
	}
}
