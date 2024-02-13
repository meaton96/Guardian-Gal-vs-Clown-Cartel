using Godot;
using System;

public partial class Interface : VBoxContainer
{
	Label text;
	Label inputText;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Label>("FeedbackLabel");
		inputText = GetNode<Label>("InputLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void AddInput(string input) {
		inputText.Text += $"\n{input}";
	}
	public void ClearInput() {
		inputText.Text = "";
	}
	public void SetFeedback(string feedback) {
		text.Text = feedback;
	}

	public void DisplayHit() {
		text.Text = "Hit :)";
	}

	public void DisplayMiss() {
		text.Text = "Miss :(";
	}
}
