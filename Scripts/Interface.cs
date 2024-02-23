using Godot;
using System;

public partial class Interface : VBoxContainer
{
	Label text;
	Label inputText;
	Label scoreText;

	Button button;
	Label platformLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Label>("FeedbackLabel");
		inputText = GetNode<Label>("InputLabel");
		scoreText = GetNode<Label>("ScoreLabel");
		platformLabel = GetNode<Label>("../PlatformNode/PlatformLabel");
		platformLabel.Text = "Platform: " + OS.GetName();
		CreateButton();
	}
	private void CreateButton()
	{
		button = GetNode<Button>("../Button/ResetButton");
		button.Pressed += ClearInput;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print("Interface Process");
	}
	public void AddInput(string input)
	{

		//inputText.Text = $"{input}\n{inputText.Text}";

	}
	public void ClearInput()
	{
		inputText.Text = "";
	}
	public void SetFeedback(string feedback)
	{
		//text.Text = feedback;
	}

	/*public void DisplayHit()
	{
		text.Text = "Hit :)";
	}*/

	public void DisplayMiss()
	{
		text.Text = "Miss :(";
	}

	public void DisplayPerfect()
	{
		text.Text = "PERFECT!! :D";
	}

	public void DisplayGreat()
	{
		text.Text = "Great!";
	}

	public void DisplayGood()
	{
		text.Text = "good";
	}

	public void DisplayScore(int score)
	{
		scoreText.Text = score.ToString();
	}
}
