using Godot;
using System;

public partial class TownNameShow : AnimatedSprite2D
{
	public static TownNameShow Instance { get; private set; }

    private Tween _activeTween;

	public override void _EnterTree()
	{
		Instance = this;
	}

	public void ShowTownSign(int frame)
    {
        _activeTween?.Kill();

        this.Frame = frame;

        var mat = (ShaderMaterial)this.Material;
        
        // Start completely hidden/faded out to lightest (+4)
        mat.SetShaderParameter("fade_offset", -4);

        _activeTween = CreateTween();
        
        // Step in from -4 to 0 over 0.3 seconds Fade in from dark
        _activeTween.TweenMethod(
            Callable.From<int>(val => mat.SetShaderParameter("fade_offset", val)),
            -4, 0, 0.3f
        );
        
        // Hold for 3 seconds
        _activeTween.TweenInterval(3.0f);
        
        // Step out from 0 back to -4 over 0.3 seconds fade out to dark
        _activeTween.TweenMethod(
            Callable.From<int>(val => mat.SetShaderParameter("fade_offset", val)),
            0, -4, 0.3f
        );
    }
}
