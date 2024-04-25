using Godot;
using System;

public partial class Fire : Node2D
{

	AnimationPlayer animPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animPlayer.Play("fire-start");
		animPlayer.Queue("fire-loop");
	}

	

	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
