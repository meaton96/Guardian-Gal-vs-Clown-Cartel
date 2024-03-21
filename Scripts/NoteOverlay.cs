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
		float remainingWidth = totalWidth;

		for (int x = 0; x < overlays.Length; x++)
		{
			float breakpoint;
			if (x == overlays.Length - 1)
			{
				breakpoint = breakpoints[^1];
			}
			else
			{
				var curBreakpoint = breakpoints[breakpoints.Count - 1 - x];
				var nextBreakpoint = breakpoints[breakpoints.Count - 2 - x];

				var breakpointDiff = nextBreakpoint - curBreakpoint;
				breakpoint = breakpointDiff;

			}
			float goalWidth = totalWidth * breakpoint;

			
			GD.Print(OverlayToString(x) + ": " + goalWidth);


			

			overlays[x].Scale = new Vector2(goalWidth / overlays[x].Texture.GetSize().X, 1);
			remainingWidth -= overlays[x].Texture.GetSize().X;
			overlays[x].Position = new Vector2(remainingWidth, -64);
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
	public void AdjustOverlay(float width, List<float> breakpoints, bool used)
	{
		// float totalWidth = width;
		// float remainingWidth = totalWidth;

		// for (int i = breakpoints.Count - 1; i >= 0; i--)
		// {
		// 	float breakpoint = breakpoints[i];
		// 	float spriteWidth = remainingWidth * breakpoint;
		// 	remainingWidth -= spriteWidth;

		// 	// Adjust sprite width
		// 	if (i == 0)
		// 	{
		// 		perfect.Scale = new Vector2(spriteWidth / perfect.Texture.GetSize().X, 1);
		// 	}
		// 	else if (i == 1)
		// 	{
		// 		float initialWidth = great.Texture.GetSize().X;
		// 		float scale = spriteWidth / initialWidth;
		// 		great.Scale = new Vector2(scale, 1);
		// 	}
		// 	else if (i == 2)
		// 	{
		// 		float initialWidth = good.Texture.GetSize().X;
		// 		float scale = spriteWidth / initialWidth;
		// 		good.Scale = new Vector2(scale, 1);
		// 	}
		// 	else if (i == 3)
		// 	{
		// 		float initialWidth = ok.Texture.GetSize().X;
		// 		float scale = spriteWidth / initialWidth;
		// 		ok.Scale = new Vector2(scale, 1);
		// 	}

		// 	// Adjust sprite position
		// 	float spriteX = totalWidth - remainingWidth - spriteWidth;
		// 	if (i == 0)
		// 		perfect.Position = new Vector2(spriteX, 0);
		// 	else if (i == 1)
		// 		great.Position = new Vector2(spriteX, 0);
		// 	else if (i == 2)
		// 		good.Position = new Vector2(spriteX, 0);
		// 	else if (i == 3)
		// 		ok.Position = new Vector2(spriteX, 0);
		// }
	}
}
