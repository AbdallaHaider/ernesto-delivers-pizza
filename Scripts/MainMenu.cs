using Godot;
using System;

public partial class MainMenu : Control
{
	private AnimatedSprite2D _cursor;

	public override void _Ready()
	{
		_cursor = GetNode<AnimatedSprite2D>("Cursor");

		TextureButton startButton = GetNode<TextureButton>("StartButton");
        TextureButton quitButton = GetNode<TextureButton>("QuitButton");

		startButton.FocusEntered += () => OnButtonFocusEntered(startButton);
        quitButton.FocusEntered += () => OnButtonFocusEntered(quitButton);

		startButton.Pressed += OnStartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;

        startButton.GrabFocus();
	}

	private void OnButtonFocusEntered(TextureButton focusedButton)
    {
        Vector2 newPos = focusedButton.GlobalPosition;
		newPos.X += focusedButton.Size.X / 2;
        newPos.Y += focusedButton.Size.Y + 10; 
        _cursor.GlobalPosition = newPos;
    }

	public void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
