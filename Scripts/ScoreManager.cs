using Godot;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; } = 0; // Starting score
	public int CurrentMultiplier {get; set;} = 1;

    // Signal to tell the UI to update
    [Signal]
    public delegate void ScoreChangedEventHandler(int newScore, int newMult);

    public override void _Ready()
    {
        Instance = this;
    }

	public void ResetMult()
	{
		CurrentMultiplier = 1;
		EmitSignal(SignalName.ScoreChanged, CurrentScore, CurrentMultiplier);
	}
	public void AddMult(int amount)
	{
		CurrentMultiplier += amount;
	}

	public void Reset()
	{
		CurrentScore = 0;
		CurrentMultiplier = 0;
	}
    public void AddScore(int amount)
    {
		if(amount < 0)
		{
			CurrentScore += amount;	
		}
		else
		{
			CurrentScore += amount * CurrentMultiplier;
			CurrentMultiplier ++;
		}
        
        // Shout that the score changed
        EmitSignal(SignalName.ScoreChanged, CurrentScore, CurrentMultiplier); 
    }
}
