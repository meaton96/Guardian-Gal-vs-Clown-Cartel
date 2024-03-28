using Godot;
using System;
using System.Collections.Generic;

public partial class NoteOverlay : Node2D
{
	Sprite2D[] overlays = new Sprite2D[4];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		overlays[3] = GetNode<Sprite2D>("OverlayPerfect");
		overlays[2] = GetNode<Sprite2D>("OverlayGreat");
		overlays[1] = GetNode<Sprite2D>("OverlayGood");
		overlays[0] = GetNode<Sprite2D>("OverlayOk");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void AdjustOverlay(float width, List<float> breakpoints)
    {
        float totalWidth = width;
        float currentPosition = 0;

        for (int x = 0; x < overlays.Length; x++)
        {
            float breakpoint = breakpoints[x];
            float goalWidth = totalWidth * breakpoint;

            overlays[x].Scale = new Vector2(goalWidth / overlays[x].Texture.GetSize().X, 1);
            overlays[x].Position = new Vector2(currentPosition, -64);

            currentPosition += goalWidth;
        }
    }
	public static string OverlayToString(int index)
	{
		return index switch
		{
			0 => "Ok",
			1 => "Good",
			2 => "Great",
			3 => "Perfect",
			_ => "Error",
		};
	}
	
}
