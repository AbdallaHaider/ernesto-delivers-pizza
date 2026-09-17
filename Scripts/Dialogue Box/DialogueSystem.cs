using Godot;
using System;

public partial class DialogueSystem : Control
{
	private RichTextLabel _textDisplay;
	private Timer _Typingtimer;
	private bool _isTyping = false;

	public override void _Ready()
	{
		_textDisplay = GetNode<RichTextLabel>("Text Box/le text");
		_Typingtimer = GetNode<Timer>("Text Box/Timer");

		_Typingtimer.Timeout += OnTypeTimerTimeout;
	}

	public void ShowMessage(String text)
	{
		Show();
		_textDisplay.Text = text;
		_textDisplay.VisibleCharacters = 0;
		_isTyping = true;
		_Typingtimer.Start();
	}

	private void OnTypeTimerTimeout()
	{
		if (_textDisplay.VisibleCharacters < _textDisplay.GetTotalCharacterCount())
		{
			_textDisplay.VisibleCharacters++;
		}
		else
		{
			_Typingtimer.Stop();
			_isTyping = false;
		}
	}
}
