using Godot;
using System;

public partial class PointHandling : Node
{
	// references
	private Interface ui;
	private Line lineDetector;

	//fields
	private int perfect = 5;
	private int great = 3;
	private int good = 1;

	public int PlayerScore { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerScore = 0;
		ui = GetNode<Interface>("../UserInterface/UserInterface");
		lineDetector = GetNode<Line>("../Line");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ui.DisplayScore(PlayerScore);
		//Miss();
	}

	public void HandleScore(Note note)
	{
		GD.Print("Point");
		// TODO: MAKE SEPERATE METHOD FOR SWIPE
		if (note.rightBound >= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 2)) &&
			note.leftBound <= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 3)))
		{
			PlayerScore += perfect;
			ui.DisplayPerfect();
		}
		else if (note.rightBound >= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 2)) &&
			note.leftBound <= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 4)))
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

	public void Miss() // TODO: Move this somewhere else once lives are implemented
	{
		GD.Print("Miss");
	}
	public void Perfect() {
		GD.Print("Perfect");
	}
	public void Great() {
		GD.Print("Great");
	}
	public void Good() {
		GD.Print("Good");
	}
	public void OK() {
		GD.Print("OK");
	}
}
