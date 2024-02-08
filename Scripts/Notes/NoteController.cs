using System.Linq;
using System;
using Godot;
using Godot.Collections;

public partial class NoteController : Container
{
	public Godot.Collections.Array existingNotes;
	private double time;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		existingNotes = new Godot.Collections.Array{};
		for (int i = 0; i < GetChildCount(); i++)
		{
			existingNotes.Add(GetChild(i));
		}
		GD.Print(existingNotes);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// For testing before we have a song to load
		time += delta;
		float roundedTime = MathF.Round((float)time, 2, MidpointRounding.ToEven);

		// Rounds a rounded number? only way I could think to get the rounding to be accurate
		// 	Rounding to 2 decimals first truncates the third digit instead of rounding it
		// Always check if node passed go
		// Every 1/4th of a second, send reg note :: 3/second
		if (roundedTime % 1 == 0.25 || 
			 roundedTime % 1 == 0.5|| 
			 roundedTime % 1 == 0.75 )
		{
			foreach(Note n in existingNotes)
			{
				if (n is RegNote && n.active == false)
				{
					//GD.Print("Spawn reg note");
					n.active = true;
					break;
				}
			}
		}
		else if (roundedTime % 5 == 0)
		{
			foreach(Note n in existingNotes)
			{
				if (n is HoldNote && n.active == false)
				{
					//GD.Print("Spawn hold note");
					n.active = true;
					break;
				}
			}
		}
		else if (roundedTime % 1 == 0)
		{
			foreach(Note n in existingNotes)
			{
				if (n is SwipeNote && n.active == false)
				{
					//GD.Print("Spawn swipe note");
					n.active = true;
					break;
				}
			}
		}
		// Every second add a swipe :: 1/second
		// Every 5 seconds add a hold :: 1/5seconds
	}
}
