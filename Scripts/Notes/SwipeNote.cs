using Godot;
using System;

public partial class SwipeNote : Note
{
	// Fields


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		xSpawnPos = -100;
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
