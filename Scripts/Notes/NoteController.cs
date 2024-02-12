using System.Linq;
using System;
using Godot;
using Godot.Collections;

public partial class NoteController : Container
{
	public Godot.Collections.Array existingNotes;
	private double time;
	const double NOTE_SPAWN_INTERVAL = 0.4;	//how often to spawn a note in seconds
	const double SWIPE_SPAWN_CHANCE = .25;	//a % base chance to spawn a swipe note each time a note is spawned
	const double HOLD_SPAWN_CHANCE = .05;	//a % base chance to spawn a hold note each time a note is spawned
	private Random random = new();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		existingNotes = new Godot.Collections.Array{};
		for (int i = 0; i < GetChildCount(); i++)
		{
			existingNotes.Add(GetChild(i));
		}
		//GD.Print(existingNotes);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleNodeSpawning(delta);
	}

	public void SpawnNote(Type type) {
		foreach(Note n in existingNotes)
		{
			if (n.GetType() == type && n.active == false)
			{
				n.active = true;
				break;
			}
		}
	
	}

	public void HandleNodeSpawning(double delta) {
		time += delta;

		if (time > NOTE_SPAWN_INTERVAL)
		{
			time = 0;
			int randomNumber = random.Next(0, 100);
			if (randomNumber < HOLD_SPAWN_CHANCE * 100)
			{
				SpawnNote(typeof(HoldNote));
			}
			else if (randomNumber < SWIPE_SPAWN_CHANCE * 100)
			{
				SpawnNote(typeof(SwipeNote));
			}
			else
			{
				SpawnNote(typeof(RegNote));
			}
		}
	}
}
