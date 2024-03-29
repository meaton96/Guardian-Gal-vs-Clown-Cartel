using Godot;
using System;
using System.Collections.Generic;

public partial class TutorialManager : Node
{
	// Tutorial text
	const string TUTORIAL_TEXT_INITIAL = "Welcome to Guardian Gal vs. Clown Cartel! Before Ellen Whiff can defend her business from the evil cartel of clowns she needs to learn how to fight.";
	const string TUTORIAL_TEXT_TAP_NOTES = "These are called 'regular notes' and they just need to be hit with a simple swing of the sword. Try 'tapping' the screen with your finger when the note reaches the sign pole!";
	const string TUTORIAL_TEXT_SWIPE_NOTES = "These are called 'swipe notes' and they need to be deflected by a sturdy shield. Try 'swiping' with your finger in any direction when the note reaches the sign pole.";
	const string TUTORIAL_TEXT_HOLD_NOTES = "These are called 'hold notes' and they need to be intercepted and held off until they can be easily tossed aside. Try 'pressing and holding' with your finger when the note reaches the sign pole. This time the note won't freeze when it hits the pole, so hold until the very end to maximize points!";
	const string TUTORIAL_TEXT_FINAL = "Now you are ready to defend your dealership from the cartel of evil clowns!";
	const string TUTORIAL_TEXT_NOTE_HIT = "Tap the screen now!";
	const string BUTTON_TEXT_INITIAL = "Ready!";
	const string BUTTON_TEXT_NOTES_PRE_SPAWN = "Let me try.";
	const string BUTTON_TEXT_NOTES_SPAWNING = "I understand, next!";
	const string BUTTON_TEXT_FINAL = "Lets Go!";
	private string displayedText = TUTORIAL_TEXT_INITIAL;

	// The NEXT part of the tutorial to be triggered when the tutorial is progressed
	private int tutorialProgressCount;
	private double timeSinceLastNoteSpawned;
	private const double TAP_NOTE_SPAWN_INTERVAL = 0.5;
	private const double SWIPE_NOTE_SPAWN_INTERVAL = 0.75;
	private const double HOLD_NOTE_SPAWN_INTERVAL = 1.5;
	private bool checkNoteDetection;

	// References
	private Sprite2D tapNote;
	private Sprite2D swipeNote;
	private Sprite2D holdNote;
	private Label tutorialTextLabel;
	private NoteController noteController;
	private Button progressTutorialButton;
	private List<Sprite2D> noteSprites;
	private Node2D uiParent;
	private Line line;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialize
		noteSprites = new List<Sprite2D>();

		GD.Print(GetTree());

		// Get References
		tutorialTextLabel = GetNode<Label>("TutorialUI/Text/TutorialTextLabel");
		uiParent = GetNode<Node2D>("../UserInterface");
		noteController = GetNode<NoteController>("../NoteController");
		line = GetNode<Line>("../Line");
		progressTutorialButton = GetNode<Button>("TutorialUI/Buttons/ProgressTutorial");
		// Adding note sprites to list
		noteSprites.Add(tapNote = GetNode<Sprite2D>("NoteSprites/TapNote"));
		noteSprites.Add(swipeNote = GetNode<Sprite2D>("NoteSprites/SwipeNote"));
		noteSprites.Add(holdNote = GetNode<Sprite2D>("NoteSprites/HoldNote"));

		// Hook-up buttons
		progressTutorialButton.Pressed += ProgressTutorial;

		// Initialize Tutorial Prompts
		tutorialProgressCount = 1;
		RefreshTutorialDisplay();
		uiParent.Visible = false;

		checkNoteDetection = false;
	}

    public override void _Process(double delta)
    {
		//GD.Print("Tuto Prog C: " + tutorialProgressCount);

		// switch (tutorialProgressCount)
		// {
		// 	// Tap note spawning
		// 	case 3:
		// 		timeSinceLastNoteSpawned += delta;
		// 		//ContinuallySpawnNotes(new RegNote(), TAP_NOTE_SPAWN_INTERVAL);
		// 	break;
		// 	// Swipe note spawning
		// 	case 5:
		// 		timeSinceLastNoteSpawned += delta;
		// 		ContinuallySpawnNotes(new SwipeNote(), SWIPE_NOTE_SPAWN_INTERVAL);
		// 	break;
		// 	// Hold note spawning
		// 	case 7:
		// 		timeSinceLastNoteSpawned += delta;
		// 		ContinuallySpawnNotes(new HoldNote(), HOLD_NOTE_SPAWN_INTERVAL);
		// 	break;
		// }

		if (checkNoteDetection && noteController.existingNotes.Count > 0 && noteController.existingNotes[0].CheckNoteHit())
		{
			noteController.existingNotes[0].Speed = 0;
			displayedText = TUTORIAL_TEXT_NOTE_HIT;
			checkNoteDetection = false;
		}

		if (tutorialProgressCount == 2)
		{

		}
    }

    /// <summary>
    /// Progresses the tutorial by 1 step
    /// </summary>
    public void ProgressTutorial()
	{
		// Checks for the current tutorial progress
		switch (tutorialProgressCount)
		{
			// Initial Case
			case 0:
				displayedText = TUTORIAL_TEXT_INITIAL;
				progressTutorialButton.Text = BUTTON_TEXT_INITIAL;
				tutorialProgressCount++;
			break;
			// Tap-Note Cases
			case 1:
				displayedText = TUTORIAL_TEXT_TAP_NOTES;
				progressTutorialButton.Text = BUTTON_TEXT_NOTES_PRE_SPAWN;
				SwapNoteSpriteTo(tapNote);
				tutorialProgressCount++;
			break;
			case 2:
				//Start tap note tutorial interactive section
				progressTutorialButton.Text = BUTTON_TEXT_NOTES_SPAWNING;
				HideAllNoteSprites();
				SpawnSingleNote(new RegNote());
				checkNoteDetection = true;
				tutorialProgressCount++;
			break;
			// Swipe-Note Cases
			case 3:
				displayedText = TUTORIAL_TEXT_SWIPE_NOTES;
				progressTutorialButton.Text = BUTTON_TEXT_NOTES_PRE_SPAWN;
				noteController.RemoveAllNotes();
				SwapNoteSpriteTo(swipeNote);
				tutorialProgressCount++;
			break;
			case 4:
				// Start swipe note tutorial interactive section
				progressTutorialButton.Text = BUTTON_TEXT_NOTES_SPAWNING;
				HideAllNoteSprites();
				SpawnSingleNote(new SwipeNote());
				checkNoteDetection = true;
				tutorialProgressCount++;
			break;
			// Hold-Note Cases
			case 5:
				displayedText = TUTORIAL_TEXT_HOLD_NOTES;
				progressTutorialButton.Text = BUTTON_TEXT_NOTES_PRE_SPAWN;
				noteController.RemoveAllNotes();
				SwapNoteSpriteTo(holdNote);
				tutorialProgressCount++;
			break;
			case 6:
				// Start hold note tutorial interactive section
				progressTutorialButton.Text = BUTTON_TEXT_NOTES_SPAWNING;
				HideAllNoteSprites();
				checkNoteDetection = false;
				SpawnSingleNote(new HoldNote());
				tutorialProgressCount++;
			break;
			// Final Case
			case 7:
				displayedText = TUTORIAL_TEXT_FINAL;
				progressTutorialButton.Text = BUTTON_TEXT_FINAL;
				noteController.RemoveAllNotes();
				tutorialProgressCount++;
			break;	
			// End Case, finishes tutorial and begins normal game
			case 8:
				EndTutorial();
			break;	
		}
		RefreshTutorialDisplay();
	}
	
	// Helper methods
	private void RefreshTutorialDisplay()
	{
		tutorialTextLabel.Text = displayedText;
	}

	/// <summary>
	/// Swaps the currently shown note sprite to the given one
	/// </summary>
	/// <param name="note"> The given note sprite to swap to </param>
	private void SwapNoteSpriteTo(Sprite2D note)
	{
		foreach (Sprite2D n in noteSprites)
		{
			n.Visible = false;
			if (n == note)
			{
				n.Visible = true;
			}
		}
	}

	/// <summary>
	/// Swaps the currently shown note sprite to the given one
	/// </summary>
	/// <param name="note"> The given note sprite to swap to </param>
	private void HideAllNoteSprites()
	{
		foreach (Sprite2D n in noteSprites)
		{
			n.Visible = false;
		}
	}

	private void ContinuallySpawnNotes(Note noteType, double spawnInterval)
	{
		//GD.Print("Entering Function to Spawn Note Type: " + noteType.GetType() + " Time Since Last Note: " + timeSinceLastNoteSpawned);
		
		if (timeSinceLastNoteSpawned > spawnInterval)
		{
			noteController.SpawnNote(noteType.GetType());
			timeSinceLastNoteSpawned = 0;
		}
	}

	private void SpawnSingleNote(Note noteType)
	{
		//GD.Print("Entering Function to Spawn Note Type: " + noteType.GetType() + " Time Since Last Note: " + timeSinceLastNoteSpawned);
		noteController.SpawnNote(noteType.GetType());
	}

	private void EndTutorial()
	{
		GD.Print("ENDING");

		uiParent.Visible = true;
		noteController.disableNoteSpawning = false;
		noteController.StartLevel();
		QueueFree();
	}
}
