using Godot;
using System;

public partial class TutorialManager : Node
{
	const string TUTORIAL_TEXT_INITIAL = "Welcome to Guardian Gal vs. Clown Cartel! Before Ellen Whiff can defend her business from the evil cartel of clowns she needs to learn how to fight.";
	const string TUTORIAL_TEXT_TAP_NOTES = "These are called 'regular notes' and they just need to be hit with a simple swing of the sword. Try 'tapping' the screen with your finger when the note reaches the pink line!";
	const string TUTORiAL_TEXT_SWIPE_NOTES = "These are called 'swipe notes' and they need to be deflected by a sturdy shield. Try 'swiping' with your finger in any direction when the note reaches the pink line.";
	const string TUTORIAL_TEXT_HOLD_NOTES = "These are called 'hold notes' and they need to be intercepted and held off until they can be easily tossed aside. Try 'pressing and holding' with your finger when the note reaches the pink line. Remember to hold until the very end to maximize points!";
	const string TUTORIAL_TEXT_FINAL = "Now you are ready to defend your dealership from the cartel of evil clowns!";	
	private string displayedText = TUTORIAL_TEXT_INITIAL;

	private Label tutorialTextLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Tutorial Begins");
		tutorialTextLabel = GetNode<Label>("UI/Text/TutorialTextLabel");
		tutorialTextLabel.Text = displayedText;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
