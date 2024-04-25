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
				//GD.Print("HIT NOTE DETECTED");
				return true;
			}
		}
		return false;
	}
	public Type GetActivteNoteType() {
		if (hitNote != null) return hitNote.GetType();
		return null;
	}
}
