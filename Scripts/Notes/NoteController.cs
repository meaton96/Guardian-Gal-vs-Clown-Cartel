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

	private const float MUSIC_DELAY = 4600; //milliseconds

	// CHANGE THIS AFTER TESTING
	public bool disableNoteSpawning = true;
	private bool justSpawnedHold = false;

	private float ySpawnPos = 300;
	private bool spawningUp = true;

	// References
	private Player player;
	//notes
	private PackedScene tapNoteScene = GD.Load<PackedScene>("res://Prefabs/tap_note.tscn");
	private PackedScene holdNoteScene = GD.Load<PackedScene>("res://Prefabs/hold_note.tscn");
	private PackedScene swipeNoteScene = GD.Load<PackedScene>("res://Prefabs/swipe_note.tscn");

	private MusicPlayer musicPlayer;

	private System.Collections.Generic.Dictionary<float, int> notes = new();
	private int index = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		existingNotes = new();
		player = GetNode<Player>("../Player");
		musicPlayer = GetNode<MusicPlayer>("../MusicPlayer");

		var song = GetSongFromJson("./audio/8-bit-circus.json");
		var beatTimesList = song.
							AsGodotDictionary()["beat_times"].
							AsFloat32Array().
							ToList();

		//GD.Print(song);
		var noteTypesList = song.AsGodotDictionary()["note_types"].AsInt32Array().ToList();

		for (int x = 0; x < beatTimesList.Count; x++)
		{
			notes.Add(beatTimesList[x], noteTypesList[x]);
		}
	}
	private async void WaitForSeconds(float seconds, Action action)
	{
		
		await ToSignal(GetTree().CreateTimer(seconds), "timeout");
		action();
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

		//	GD.Print("json: " + json);


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
		if (disableNoteSpawning == false)
		{
			HandleNoteSpawning(delta);
		}
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
			(instance as Note).Visible = true;



			ySpawnPos += spawningUp ? 30 : -30;
			if (ySpawnPos >= GetViewportRect().Size.Y - 100 || ySpawnPos <= 100)
			{
				// start decreasing spawnPosY
				spawningUp = !spawningUp;
			}
			(instance as Note).EnableNote(ySpawnPos);
			existingNotes.Add(instance as Note);

			//GD.Print($"Spawned a {instance.GetType()}");
		}
	}
	public void StartLevel()
	{
		WaitForSeconds(MUSIC_DELAY / 1000, () =>
		{
			musicPlayer.Play();
		});
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

	public void RemoveAllNotes()
	{
		foreach (Note n in existingNotes)
		{
			n.QueueFree();
		}
		existingNotes.Clear();
	}

	public void HandleNoteSpawning(double delta)
	{
		time += delta;

		if (index < notes.Keys.Count && time >= notes.ElementAt(index).Key)
		{
			//currentNoteSpawnInterval = DEFAULT_NOTE_SPAWN_INTERVAL;
			switch (notes[notes.ElementAt(index).Key])
			{
				case 0:
					SpawnNote(typeof(RegNote));
					break;
				case 1:
					SpawnNote(typeof(SwipeNote));
					break;
				case 2:
					SpawnNote(typeof(HoldNote));
					break;
			}
			index++;
		}
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
}
