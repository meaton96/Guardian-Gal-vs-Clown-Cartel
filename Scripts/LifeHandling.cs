using Godot;
using System;

public partial class LifeHandling : Node
{
	private Interface ui;
	private Line lineDetector;

	public int PlayerHealth {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerHealth = 3;
		ui = GetNode<Interface>("../UserInterface/UserInterface");
		lineDetector = GetNode<Line>("../Line");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ShowHealth();
	}

	public void ShowHealth()
	{
		switch(PlayerHealth)
		{
			case 3:
			break;

			case 2:
			break;

			case 1:
			break;

			// death
			default:
			break;
		}
	}
}
