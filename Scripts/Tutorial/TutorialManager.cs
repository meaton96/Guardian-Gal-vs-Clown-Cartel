using Godot;
using System;
using System.Collections.Generic;

public partial class TutorialManager : Node
{
	// Tutorial text
	const string TUTORIAL_TEXT_INITIAL = "Welcome to Guardian Gal vs. Clown Cartel! Before Ellen Whiff can defend her business from the evil cartel of clowns she needs to learn how to fight.";
	const string TUTORIAL_TEXT_TAP_NOTES = "These are called 'regular notes' and they just need to be hit with a simple swing of the sword. Try 'tapping' the screen with your finger when the note reaches the pink line!";
	const string TUTORIAL_TEXT_SWIPE_NOTES = "These are called 'swipe notes' and they need to be deflected by a sturdy shield. Try 'swiping' with your finger in any direction when the note reaches the pink line.";
	const string TUTORIAL_TEXT_HOLD_NOTES = "These are called 'hold notes' and they need to be intercepted and held off until they can be easily tossed aside. Try 'pressing and holding' with your finger when the note reaches the pink line. Remember to hold until the very end to maximize points!";
	const string TUTORIAL_TEXT_FINAL = "Now you are ready to defend your dealership from the cartel of evil clowns!";
	private string displayedText = TUTORIAL_TEXT_INITIAL;

	private int tutorialProgressCount;
	// References
	private Sprite2D tapNote;
	// References
	private Sprite2D swipeNote;
	// References
	private Sprite2D holdNote;
	private Label tutorialTextLabel;
	private NoteController noteController;
	private Button progressTutorialButton;
	private List<Sprite2D> noteSprites;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Initialize
		noteSprites = new List<Sprite2D>();

		// Get References
		tutorialTextLabel = GetNode<Label>("UI/Text/TutorialTextLabel");
		noteController = GetNode<NoteController>("NoteController");
		progressTutorialButton = GetNode<Button>("UI/Buttons/ProgressTutorial");
		// Adding note sprites to list
		noteSprites.Add(tapNote = GetNode<Sprite2D>("NoteSprites/TapNote"));
		noteSprites.Add(swipeNote = GetNode<Sprite2D>("NoteSprites/SwipeNote"));
		noteSprites.Add(holdNote = GetNode<Sprite2D>("NoteSprites/HoldNote"));

		// Hook-up buttons
		progressTutorialButton.Pressed += ProgressTutorial;

		// Initialize Tutorial Prompts
		tutorialProgressCount = 1;
		RefreshTutorialDisplay();
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
				tutorialProgressCount++;
			break;
			// Tap-Note Cases
			case 1:
				displayedText = TUTORIAL_TEXT_TAP_NOTES;
				SwapNoteSpriteTo(tapNote);
				tutorialProgressCount++;
			break;
			case 2:
				//Start tap note tutorial interactive section
			break;
			// Swipe-Note Cases
			case 3:
				displayedText = TUTORIAL_TEXT_SWIPE_NOTES;
				SwapNoteSpriteTo(swipeNote);
				tutorialProgressCount++;
			break;
			case 4:
				// Start swipe note tutorial interactive section
			break;
			// Hold-Note Cases
			case 5:
				displayedText = TUTORIAL_TEXT_HOLD_NOTES;
				SwapNoteSpriteTo(holdNote);
				tutorialProgressCount++;
			break;
			case 6:
				// Start hold note tutorial interactive section
			break;
			// Final Case
			case 7:
				displayedText = TUTORIAL_TEXT_FINAL;
				tutorialProgressCount++;
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
}
