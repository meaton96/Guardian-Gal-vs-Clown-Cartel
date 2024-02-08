using Godot;
using System;

public partial class Line : Container
{
	// When click/tap/hold, check if there is a note touching the line
	//	Check if it is within the perfect segment
	// 	If so, give perfect feedback, if just in regular line bounds give regular feedback
	//  

	private NoteController noteController;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		noteController = GetNode<NoteController>("NoteController");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	// Returns false if there is no note detected after checking tap
	public bool CheckTap()
	{
		foreach (Note n in noteController.existingNotes)
		{
			if (n is RegNote && n.active && n.GlobalPosition.X >= (GlobalPosition.X - Size.X) && n.GlobalPosition.X <= (GlobalPosition.X + Size.X))
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckHold()
	{
		foreach (Note n in noteController.existingNotes)
		{
			if (n is HoldNote && n.active && n.GlobalPosition.X >= (GlobalPosition.X - Size.X) && n.GlobalPosition.X <= (GlobalPosition.X + Size.X))
			{
				return true;
			}
		}
		return false;
	}

	public bool CheckSwipe()
	{
		foreach (Note n in noteController.existingNotes)
		{
			if (n is SwipeNote && n.active && n.GlobalPosition.X >= (GlobalPosition.X - Size.X) && n.GlobalPosition.X <= (GlobalPosition.X + Size.X))
			{
				return true;
			}
		}
		return false;
	}
}
