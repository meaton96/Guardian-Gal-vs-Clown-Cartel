using Godot;
using System;

public partial class Interface : VBoxContainer
{
	Label text;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Label>("FeedbackLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void DisplayHit() {
		text.Text = "Hit :)";
	}

	public void DisplayMiss() {
		text.Text = "Miss :(";
	}
}
