using Godot;
using System;

public partial class PointHandling : Node
{
	// references
	private Interface ui;

	// fields
	public float greatRightBound;
	public float greatLeftbound;
	public float perfectRightBound;
	public float perfectLeftBound;

	private int perfect;
	private int great;
	private int good;

	public int PlayerScore { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerScore = 0;
		ui = GetNode<Interface>("UserInterface/UserInterface");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ui.DisplayScore(PlayerScore);
	}

	public void HandleScore(Note note, float lineLocation, float lineSize)
	{
		// the fractions change where each notes is detected
		if(perfectRightBound >= (lineLocation - (lineSize * 1/4)) && 
			perfectLeftBound <= (lineLocation+ (lineSize * 1/4)))
		{
			PlayerScore += perfect;
			ui.DisplayPerfect();
		}
		else if(greatRightBound >= (lineLocation - (lineSize * 1/3)) && 
			greatLeftBound <= (lineLocation+ (lineSize * 1/3)))
		{
			PlayerScore += great;
			ui.DisplayGreat();
		}
		else
		{
			PlayerScore += good;
			ui.DisplayGood();
		}
	}
}
