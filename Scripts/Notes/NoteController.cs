using System.Linq;
using System;
using System.IO;
using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class NoteController : Container
{
	public List<Note> existingNotes;
	private double time;
	private double currentNoteSpawnInterval;
	const double DEFAULT_NOTE_SPAWN_INTERVAL = 0.6; //how often to spawn ANY note in seconds
	const double HOLD_NOTE_SPAWN_INTERVAL = 1.1; //how often to spawn a HOLD note in seconds
	const double SWIPE_SPAWN_CHANCE = .25;  //a % base chance to spawn a swipe note each time a note is spawned
	const double HOLD_SPAWN_CHANCE = .10;   //a % base chance to spawn a hold note each time a note is spawned


	private bool justSpawnedHold = false;
	private Random random = new();

	private Player player;

	private List<float> beatTimesList = new();
	private int index = 0;


	//notes
	private PackedScene tapNoteScene = GD.Load<PackedScene>("res://Prefabs/tap_note.tscn");
	private PackedScene holdNoteScene = GD.Load<PackedScene>("res://Prefabs/hold_note.tscn");
	private PackedScene swipeNoteScene = GD.Load<PackedScene>("res://Prefabs/swipe_note.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		existingNotes = new();
		player = GetNode<Player>("../");


		beatTimesList = GetSongFromJson("./audio/red-lips.json").
							AsGodotDictionary()["beat_times"].
							AsFloat32Array().
							ToList();




	}

	public static Variant GetSongFromJson(string path)
	{
		string json = "";
		try
		{
			//Pass the file path and file name to the StreamReader constructor
			StreamReader sr = new(path);
			//Read the first line of text
			json = sr.ReadToEnd();
			//close the file
			sr.Close();
		}
		catch (Exception e)
		{
			GD.Print("Exception: " + e.Message);

		}


		return Json.ParseString(json);
	}
	// public List<float> GetBeatTimesFromObject(Variant obj)
	// {
	// 	var beatTimesArray = null;
	// 	//var beatTimesList = beatTimes.Cast<float>().ToList();
	// 	return beatTimesList;
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleNoteSpawning(delta);
		HandleNoteRemoving();
	}

	// public void SpawnNote(Type type) {
	// 	foreach(Note n in existingNotes)
	// 	{
	// 		if (n.GetType() == type && n.active == false)
	// 		{
	// 			n.EnableNote();
	// 			break;
	// 		}
	// 	}
	// }
	public void SpawnNote(Type type)
	{
		PackedScene scene = type.Name switch
		{
			"RegNote" => tapNoteScene,
			"HoldNote" => holdNoteScene,
			"SwipeNote" => swipeNoteScene,
			_ => null,
		};
		if (scene != null)
		{
			var instance = scene.Instantiate();
			AddChild(instance);
			(instance as Note).EnableNote();
			existingNotes.Add(instance as Note);

			//	GD.Print($"Spawned a {instance.GetType()}");
		}
	}

	public void HandleNoteRemoving()
	{
		var nodesToRemove = existingNotes.FindAll(note => !note.active);

		nodesToRemove.ForEach(note =>
		{
			existingNotes.Remove(note);
			note.QueueFree();
		});

	}


	// public void HandleNoteSpawning(double delta)
	// {
	// 	time += delta;

	// 	if (time > currentNoteSpawnInterval)
	// 	{
	// 		time = 0;

	// 		if (justSpawnedHold)
	// 		{
	// 			justSpawnedHold = false;
	// 			return;
	// 		}

	// 		int randomNumber = random.Next(0, 100);
	// 		if (randomNumber < HOLD_SPAWN_CHANCE * 100)
	// 		{
	// 			currentNoteSpawnInterval = HOLD_NOTE_SPAWN_INTERVAL;
	// 			SpawnNote(typeof(HoldNote));
	// 			justSpawnedHold = true;
	// 		}
	// 		else if (randomNumber < SWIPE_SPAWN_CHANCE * 100)
	// 		{
	// 			currentNoteSpawnInterval = DEFAULT_NOTE_SPAWN_INTERVAL;
	// 			SpawnNote(typeof(SwipeNote));
	// 		}
	// 		else
	// 		{
	// 			currentNoteSpawnInterval = DEFAULT_NOTE_SPAWN_INTERVAL;
	// 			SpawnNote(typeof(RegNote));
	// 		}
	// 	}
	// }
	public void HandleNoteSpawning(double delta)
	{
		time += delta;
		if (index < beatTimesList.Count && time >= beatTimesList[index])
		{
			//currentNoteSpawnInterval = DEFAULT_NOTE_SPAWN_INTERVAL;
			SpawnNote(typeof(RegNote));
			index++;
		}
	}
}
