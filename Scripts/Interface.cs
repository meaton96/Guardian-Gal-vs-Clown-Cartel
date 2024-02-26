using Godot;
using System;

public partial class Interface : VBoxContainer
{
	Label text;
	//Label inputText;
	Label scoreText;

	Button resetButton, pauseButton, resumeButton;

	Node2D pauseMenu;
	//Label platformLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Label>("FeedbackLabel");
		scoreText = GetNode<Label>("ScoreLabel");
		pauseMenu = GetNode<Node2D>("PauseMenu");
		CreateButton();
	}
	private void CreateButton()
	{
		resetButton = GetNode<Button>("../Button/ResetButton");
		resetButton.Pressed += ClearInput;

		pauseButton = GetNode<Button>("../PauseButton/PauseButton");
		pauseButton.Pressed += PauseGame;

		resumeButton = GetNode<Button>("PauseMenu/PauseContainer/ResumeButton");
		resumeButton.Pressed += OnCloseButtonPressed;

	}
	public void PauseGame()
	{
		GD.Print("PauseGame");
		GetTree().Paused = !GetTree().Paused;
		pauseMenu.Show();
	}
	private void OnCloseButtonPressed()
	{
		GD.Print("OnCloseButtonPressed");
		GetTree().Paused = false;
		pauseMenu.Hide();
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
		//inputText.Text = "";
	}

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
