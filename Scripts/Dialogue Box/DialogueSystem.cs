using Godot;
using System;

public partial class DialogueSystem : Control
{
	public static DialogueSystem Instance { get; private set; }

	private RichTextLabel _textDisplay;
	private Timer _typingTimer;
	private bool _isTyping = false;

	private string[] _dialoguePages;
    private int _currentPage = 0;

	public override void _Ready()
	{
		Instance = this; // Makes this specific dialogue box globally accessible

		_textDisplay = GetNode<RichTextLabel>("Text Box/le text");
		_typingTimer = GetNode<Timer>("Text Box/Timer");

		_typingTimer.Timeout += OnTypeTimerTimeout;
	}

	public void ShowMessage(string text)
	{
		
	}

	public void ShowMessage(string[] text)
	{
		_dialoguePages = text;
        _currentPage = 0;
        
        Show();
        DisplayCurrentPage();
	}

	private void DisplayCurrentPage()
    {
        _textDisplay.Text = _dialoguePages[_currentPage];
        _textDisplay.VisibleCharacters = 0;
        _isTyping = true;
        
        _typingTimer.WaitTime = 0.05;
        _typingTimer.Start();
    }

	private void OnTypeTimerTimeout()
	{
		if (_textDisplay.VisibleCharacters < _textDisplay.GetTotalCharacterCount())
		{
			_textDisplay.VisibleCharacters++;
		}
		else
		{
			_typingTimer.Stop();
			_isTyping = false;
		}
	}

    public override void _Input(InputEvent @event)
    {
		if (!Visible) return; //pra nao rodar o input se nem tem dialogue box na tela

        if (@event.IsActionPressed("ui_accept"))
		{
			if (_isTyping)
			{
				_typingTimer.WaitTime = 0.02;
			}
			else
			{
				_currentPage++;
                
                if (_currentPage < _dialoguePages.Length)
                {
                    DisplayCurrentPage();
                }
                else
                {
                    Hide();
                }
			}
		}
		else if (@event.IsActionReleased("ui_accept")) 
        {
            _typingTimer.WaitTime = 0.05;
        }
    }

}
