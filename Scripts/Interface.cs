using Godot;
using System;

public partial class Interface : VBoxContainer
{
	//Label text;
	//Label inputText;
	Sprite2D perfect;
	Sprite2D great;
	Sprite2D good;
	Sprite2D ok;
	Sprite2D tooSlow;

	Label scoreText;
	Label streakText;

	Button pauseButton, resumeButton, restartButton, backButton;

	MusicPlayer musicPlayer;
 	NoteController noteController;
	MainMenu mainMenu;

	Node2D pauseMenu;
	//Label platformLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//text = GetNode<Label>("FeedbackLabel");
		scoreText = GetNode<Label>("ScoreAnchor/score-anchor/ScoreLabel");
		streakText = GetNode<Label>("ScoreAnchor/streak-anchor/StreakLabel");
		pauseMenu = GetNode<Node2D>("PauseMenu");
		musicPlayer = GetNode<MusicPlayer>("../../MusicPlayer");
		mainMenu = GetNode<MainMenu>("../../../Menus/MainMenu");

		noteController = GetNode<NoteController>("../../NoteController");

		perfect = GetNode<Sprite2D>("PerfectFeedback");
		perfect.Visible = false;
		great = GetNode<Sprite2D>("GreatFeedback");
		great.Visible = false;
		good = GetNode<Sprite2D>("GoodFeedback");
		good.Visible = false;
		ok = GetNode<Sprite2D>("OkFeedback");
		ok.Visible = false;
		tooSlow = GetNode<Sprite2D>("TooSlowFeedback");
		tooSlow.Visible = false;

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

		backButton = GetNode<Button>("PauseMenu/PauseContainer/BackButton");
		//backButton.Pressed += mainMenu.OpenMainMenu;
	}
	public void RestartSong()
	{
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
		perfect.Visible = false;
		great.Visible = false;
		good.Visible = false;
		ok.Visible = false;
		tooSlow.Visible = true;
	}

	public void DisplayPerfect()
	{
		//text.Text = "PERFECT!! :D";
		perfect.Visible = true;
		great.Visible = false;
		good.Visible = false;
		ok.Visible = false;
		tooSlow.Visible = false;
	}

	public void DisplayGreat()
	{
		//text.Text = "Great!";
		perfect.Visible = false;
		great.Visible = true;
		good.Visible = false;
		ok.Visible = false;
		tooSlow.Visible = false;
	}

	public void DisplayGood()
	{
		//text.Text = "good";
		perfect.Visible = false;
		great.Visible = false;
		good.Visible = true;
		ok.Visible = false;
		tooSlow.Visible = false;
	}

	public void DisplayOk()
	{
		//text.Text = "good";
		perfect.Visible = false;
		great.Visible = false;
		good.Visible = false;
		ok.Visible = true;
		tooSlow.Visible = false;
	}

	public void DisplayScore(int score)
	{
		scoreText.Text = score.ToString();
	}
	public void DisplayStreak(int streak)
	{
		streakText.Text = streak.ToString();
	}
}
