using Godot;
using System;

public partial class Interface : VBoxContainer
{
	Label text;
	//Label inputText;
	Label scoreText;

	Button pauseButton, resumeButton, restartButton;

	MusicPlayer musicPlayer;
	NoteController noteController;

	Node2D pauseMenu;
	//Label platformLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		text = GetNode<Label>("FeedbackLabel");
		scoreText = GetNode<Label>("ScoreLabel");
		pauseMenu = GetNode<Node2D>("PauseMenu");
		musicPlayer = GetNode<MusicPlayer>("../../MusicPlayer");

		noteController = GetNode<NoteController>("../../NoteController");


        
		CreateButton();
	}
	private void CreateButton()
	{
		// resetButton = GetNode<Button>("../Button/ResetButton");
		// resetButton.Pressed += ClearInput;

		pauseButton = GetNode<Button>("../PauseButton/PauseButton");
		pauseButton.Pressed += PauseGame;

		resumeButton = GetNode<Button>("PauseMenu/PauseContainer/ResumeButton");
		resumeButton.Pressed += OnCloseButtonPressed;

		restartButton = GetNode<Button>("PauseMenu/PauseContainer/RestartButton");
        restartButton.Pressed += RestartSong;



	}
	public void RestartSong() {
        noteController.StartLevel();
        OnCloseButtonPressed();
        
    }
	public void PauseGame()
	{
		GD.Print("PauseGame");
		var paused = GetTree().Paused;
		//musicPlayer.StreamPaused = !paused;
		GetTree().Paused = !paused;
		pauseMenu.Visible = !paused;
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
	// public void ClearInput()
	// {
	// 	//inputText.Text = "";
	// }

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
