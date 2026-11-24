using Godot;
using System;
using System.Runtime.Intrinsics;

public partial class HouseExclamation : Sprite2D
{
    [Export] public float TopUiHeight = 10.0f;
	[Export] public Texture2D smallExclamation;
			private Texture2D normalExclamation;
			public float offscreenLeeway = 2.0f;
	//eclamation positoions
    private Vector2 _defaultOffset;
    private Node2D _house;

	private Vector2 _normalBaseHalfSize;

    public override void _Ready()
    {
        //original position
        _defaultOffset = Position;
        _house = GetParent<Node2D>();

		normalExclamation = Texture;
		_normalBaseHalfSize = GetRect().Size * 0.5f;
    }

    public override void _Process(double delta)
    {
        if (!Visible || _house == null) return;

        //icon position above the house
        Vector2 targetPos = _house.GlobalPosition + _defaultOffset;

		Transform2D invTransform = GetCanvasTransform().AffineInverse();
		Vector2 playableTopLeft = invTransform * new Vector2(0, TopUiHeight);
        Vector2 playableBottomRight = invTransform * GetViewportRect().Size;

        //didnt even need this lol
		Vector2 normalHalfSize = _normalBaseHalfSize * GlobalScale.Abs();
        bool isOffscreen =
            targetPos.X < playableTopLeft.X ||
            targetPos.X > playableBottomRight.X  ||
            targetPos.Y < playableTopLeft.Y||
            targetPos.Y > playableBottomRight.Y;

		Texture2D setTexture = (isOffscreen && smallExclamation != null)
		? smallExclamation : normalExclamation;

		if (Texture != setTexture)
        {
            Texture = setTexture;
        }

		//so that the sprite doesnt clip outside
		Vector2 currentHalfSize = GetRect().Size * GlobalScale.Abs() * 0.5f;

        //Inset every edge by halfSize so the full sprite stays visible and flush
		float clampedX = Mathf.Clamp(
            targetPos.X,
            playableTopLeft.X + currentHalfSize.X,
            playableBottomRight.X - currentHalfSize.X
        );
        float clampedY = Mathf.Clamp(
            targetPos.Y,
            playableTopLeft.Y + currentHalfSize.Y,
            playableBottomRight.Y - currentHalfSize.Y
        );

        GlobalPosition = new Vector2(clampedX, clampedY);
    }
}
