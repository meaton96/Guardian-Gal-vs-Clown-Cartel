using Godot;
using System;
using System.Reflection;

public partial class Line : Container
{
	private NoteController noteController;

	public Note hitNote;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		noteController = GetNode<NoteController>("../NoteController");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	// Returns false if there is no note detected after checking tap
	// public bool CheckTap()
	// {
	// 	foreach (Note n in noteController.existingNotes)
	// 	{
	// 		// Check if the note is a regular note, if its active, 
	// 		//	and if the line is at all within the bounds of the note (AABB, but only one axis)
	// 		if (n is RegNote && n.CheckNoteHit())
	// 		{
	// 			hitNote = n;
	// 			pointController.HandleScore(n);
	// 			return true;
	// 		}
	// 		else if (n is RegNote && !n.CheckNoteHit())
	// 		{
	// 			pointController.Miss();
	// 		}
	// 	}
	// 	return false;
	// }

	// public bool CheckHold()
	// {
	// 	foreach (Note n in noteController.existingNotes)
	// 	{
	// 		// Check if the note is a hold note, if its active, 
	// 		//	and if the line is at all within the bounds of the note (AABB, but only one axis)
	// 		if (n is HoldNote && n.CheckNoteHit())
	// 		{
	// 			hitNote = n;
	// 			pointController.HandleScore(n);
	// 			return true;
	// 		}
	// 		else if (n is HoldNote && !n.CheckNoteHit())
	// 		{
	// 			pointController.Miss();
	// 		}
	// 	}
	// 	return false;
	// }

	// public bool CheckSwipe()
	// {
	// 	foreach (Note n in noteController.existingNotes)
	// 	{
	// 		// Check if the note is a swipe note, if its active, 
	// 		//	and if the line is at all within the bounds of the note (AABB, but only one axis)
	// 		if (n is SwipeNote && n.CheckNoteHit())
	// 		{
	// 			hitNote = n;
	// 			pointController.HandleScore(n);
	// 			return true;
	// 		}
	// 		else if (n is SwipeNote && !n.CheckNoteHit())
	// 		{
	// 			pointController.Miss();
	// 		}
	// 	}
	// 	return false;
	// }

	/// <summary>
	/// Checks if there is a note over the line at all
	/// </summary>
	/// <returns></returns>
	public bool CheckNoteDetection()
	{
		foreach (Note n in noteController.existingNotes)
		{
			if (n.CheckNoteHit())
			{
				hitNote = n;
				GD.Print("HIT NOTE DETECTED");
				return true;
			}
		}
		return false;
	}
}
