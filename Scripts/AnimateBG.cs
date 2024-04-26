using Godot;
using System;
using System.Collections;
using System.Diagnostics;

public partial class AnimateBG : Sprite2D
{
	// References
	private Node2D YPGImage;
	private Node2D GYPImage;
	private Node2D PGYImage;
	private Queue backgrounds;
	private int frameCounter;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Penis");
		// Get sprite nodes
		YPGImage = (Node2D)GetNode("YPG");
		GYPImage = (Node2D)GetNode("GYP");
		PGYImage = (Node2D)GetNode("PGY");
		YPGImage.Visible = false;
		GYPImage.Visible = false;
		PGYImage.Visible = true;

		// Set up initial queue
		backgrounds = new Queue();
		backgrounds.Enqueue(YPGImage);
		backgrounds.Enqueue(GYPImage);
		backgrounds.Enqueue(PGYImage);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (frameCounter > 50)
		{
			Node2D tempBG = (Node2D)backgrounds.Dequeue();
			foreach (Node2D bg in backgrounds)
			{
				bg.Visible = false;
			}
			tempBG.Visible = true;
			backgrounds.Enqueue(tempBG);
			frameCounter = 0;
		}
		frameCounter++;
	}
}
