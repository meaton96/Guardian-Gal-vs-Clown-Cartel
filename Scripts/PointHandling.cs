using Godot;
using System;
using System.Collections;
using System.Resources;

public partial class PointHandling : Node
{
	// references
	private Interface ui;
	private Line lineDetector;

	private Sprite2D[] clowns;

	private FireGroups fireGroups;

	private int streak = 0;
	private int streakMultiplier = 1;
	private int[] streakMultiplierBreakpoints = { 5, 20, 35 };
	private const float CLOWN_MOVE_SPEED = 30;

	//fields
	private int perfect = 10;
	private int great = 5;
	private int good = 3;
	private int okay = 1;

	public int PlayerScore { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireGroups = GetNode<FireGroups>("fire-groups");
		clowns = new Sprite2D[]{
			GetNode<Sprite2D>("../Sprites/Clown"), GetNode<Sprite2D>("../Sprites/Clown2"),
			};

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

	// public void HandleScore(Note note)
	// {
	// 	GD.Print("Point");
	// 	// TODO: MAKE SEPERATE METHOD FOR SWIPE
	// 	if (note.rightBound >= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 2)) &&
	// 		note.leftBound <= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 3)))
	// 	{
	// 		PlayerScore += perfect * streakMultiplier;
	// 		ui.DisplayPerfect();
	// 	}
	// 	else if (note.rightBound >= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 2)) &&
	// 		note.leftBound <= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 4)))
	// 	{
	// 		PlayerScore += great * streakMultiplier;;
	// 		ui.DisplayGreat();
	// 	}
	// 	else
	// 	{
	// 		PlayerScore += good * streakMultiplier;;
	// 		ui.DisplayGood();
	// 	}

	// }



	private void UpdateStreak(bool reset = false)
	{
		//GD.Print("streak: " + streak + " streakMultiplier: " + streakMultiplier);
		if (reset)
		{
			streak = 0;
			if (streakMultiplier > 1)
			{
				streakMultiplier = 1;
				//fireGroups.DisableAllGroups();
				MoveClowns(Vector2.Right, 2f);
			}
			return;
		}
		streak++;
		if (streak > streakMultiplierBreakpoints[2])
		{
			if (streakMultiplier < 5)
			{
				fireGroups.EnableGroup(2);
				MoveClowns(Vector2.Left, 2f);
				streakMultiplier = 5;
			}
		}
		else if (streak > streakMultiplierBreakpoints[1])
		{
			if (streakMultiplier < 3)
			{
				fireGroups.EnableGroup(1);
				MoveClowns(Vector2.Left, 1.5f);
				streakMultiplier = 3;
			}
		}
		else if (streakMultiplier < 2 && streak > streakMultiplierBreakpoints[0])
		{
			if (streakMultiplier < 2)
			{
				MoveClowns(Vector2.Left, 1f);
				fireGroups.EnableGroup(0);
				streakMultiplier = 2;
			}
		}
		ui.DisplayStreak(streak);
		
	}
	private void MoveClowns(Vector2 direction, float speed = 1.0f)
	{
		foreach (Sprite2D clown in clowns)
		{
			clown.Position += direction * CLOWN_MOVE_SPEED * speed;
			if (clown.Position.X >= 2050) {
				GD.Print("Clown kill u");
			}
		}

	}

	public void Miss() // TODO: Move this somewhere else once lives are implemented
	{
		//GD.Print("Miss");
		MoveClowns(Vector2.Right);
		ui.DisplayMiss();
		UpdateStreak(true);
	}
	public void Perfect()
	{
		//GD.Print("Perfect");
		UpdateStreak();
		PlayerScore += perfect * streakMultiplier;
		ui.DisplayPerfect();
	}
	public void Great()
	{
		//GD.Print("Great");
		UpdateStreak();
		PlayerScore += great * streakMultiplier;
		ui.DisplayGreat();
	}
	public void Good()
	{
		//GD.Print("Good");
		UpdateStreak();

		PlayerScore += good * streakMultiplier;
		ui.DisplayGood();
	}
	public void OK()
	{
		//GD.Print("OK");
		UpdateStreak();

		PlayerScore += okay * streakMultiplier;
		ui.DisplayOk();
	}
}
