using Godot;
using NAudio.Wave;
using System;

public partial class MainMenu : Control
{
	private Button playButton;
	private Button settingsButton;
	private Button tutorialButton;
	private Node2D game;
	private Node2D tutorial;
	private TutorialManager tutorialScript;
	private Control settingsMenu;
	private NoteController noteController;

	private const float SPEED = 0.1f;
	private float velocity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playButton = GetNode<Button>("PlayButton");
		playButton.Pressed += Play;
		
		settingsButton = GetNode<Button>("SettingsButton");
		settingsButton.Pressed += OpenSettings;

		tutorialButton = GetNode<Button>("TutorialButton");
		tutorialButton.Pressed += PlayTutorial;

		game = GetNode<Node2D>("../../Game");
		noteController = (NoteController)game.GetChild(2);
		game.Visible = false;

		tutorialScript = GetNode<TutorialManager>("../../Game/Tutorial");;
		tutorial = GetNode<Node2D>("../../Game/Tutorial");

		settingsMenu = GetNode<Control>("../SettingsMenu");
		settingsMenu.Visible = false;

		velocity = -SPEED;
		tutorialScript.DisableTutorial();
		tutorialScript.progressTutorialButton.Disabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		playButton.Position += new Vector2(0, velocity);
		settingsButton.Position += new Vector2(0, velocity);
		tutorialButton.Position += new Vector2(0, velocity);

		if (playButton.Position.Y >= 500)
		{
			velocity = -SPEED;
		}
		if (playButton.Position.Y <= 480)
		{
			velocity = SPEED;
		}
	}

	private void Play()
	{
		GD.Print("Playing!");
		this.Visible = false;
		game.Visible = true;
		settingsMenu.Visible = false;
		tutorial.Visible = false;
		tutorialScript.DisableTutorial();
		noteController.disableNoteSpawning = false;
		noteController.StartLevel();
	}
	private void OpenSettings()
	{
		GD.Print("Settings!");
		this.Visible = false;
		settingsMenu.Visible = true;
		tutorial.Visible = false;
		tutorialScript.DisableTutorial();
	}
	private void PlayTutorial()
	{
		GD.Print("Tutorial!");
		this.Visible = false;
		settingsMenu.Visible = false;
		game.Visible = true;
		tutorial.Visible = true;
		tutorialScript.progressTutorialButton.Disabled = false;
		tutorialScript.EnableTutorial();
	}
}