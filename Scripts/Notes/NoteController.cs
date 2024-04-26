using System.Linq;
using System;
using Godot;
using System.IO;
using Godot.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class NoteController : Container
{
	private const string TEMP_JSON = "{\"beat_times\": [0.3439229,0.93666667,1.1589569,1.6397732,2.4980044,2.5599546,3.4326077,3.5062132,4.363651,4.563991,5.25966,5.661383,6.0834923,6.9559865,7.030476,7.8359184,8.4652605,8.719773,9.201201,9.639796,10.491542,10.512653,11.443673,11.641564,12.283538,12.741293,13.1634245,13.781701,14.052585,14.915873,15.00898,15.799501,16.239954,16.719728,17.322767,17.661678,18.4817,18.726303,19.43805,19.891066,20.24347,20.863447],\"note_types\": [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"tempo\": 136}";
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

	private PauseMenu pauseMenu;
	//notes
	private PackedScene tapNoteScene = GD.Load<PackedScene>("res://Prefabs/tap_note.tscn");
	private PackedScene holdNoteScene = GD.Load<PackedScene>("res://Prefabs/hold_note.tscn");
	private PackedScene swipeNoteScene = GD.Load<PackedScene>("res://Prefabs/swipe_note.tscn");

	private MusicPlayer musicPlayer;

	private System.Collections.Generic.Dictionary<float, int> levelNotes = new();
	private int index = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		existingNotes = new();
		player = GetNode<Player>("../Player");
		musicPlayer = GetNode<MusicPlayer>("../MusicPlayer");
		pauseMenu = GetNode<PauseMenu>("../UserInterface/UserInterface/PauseMenu/PauseContainer");
		
		LoadSong();
		
	}
	private async void WaitForSeconds(float seconds, Action action)
	{
		
		await ToSignal(GetTree().CreateTimer(seconds), "timeout");
		action();
	}
	

	public void LoadSong() {

		// string path = "res://audio/8-bit-circus.json";
		// string folderPath = "res://audio";

		// if (!DoesFolderExist(folderPath))
		// {
		// 	var dir = Directory.CreateDirectory(ProjectSettings.GlobalizePath(folderPath));
		// 	GD.Print($"created audio folder at {dir.FullName}");
		// 	string fullPath = ProjectSettings.GlobalizePath(path);
		// 	File.WriteAllText(fullPath, TEMP_JSON);
		// 	GD.Print("wrote temp json");
		// }


		// //var song = GetSongFromJson(path);
		// var song  = Json.ParseString(TEMP_JSON);
		// var beatTimesList = song.
		// 					AsGodotDictionary()["beat_times"].
		// 					AsFloat32Array().
		// 					ToList();

		List<float> beatTimesList = new List<float>()
		{
			0.3439229f, 0.93666667f, 1.1589569f, 1.6397732f, 2.4980044f, 2.5599546f, 3.4326077f, 3.5062132f, 4.363651f, 4.563991f,
			5.25966f, 5.661383f, 6.0834923f, 6.9559865f, 7.030476f, 7.8359184f, 8.4652605f, 8.719773f, 9.201201f, 9.639796f,
			10.491542f, 10.512653f, 11.443673f, 11.641564f, 12.283538f, 12.741293f, 13.1634245f, 13.781701f, 14.052585f, 14.915873f,
			15.00898f, 15.799501f, 16.239954f, 16.719728f, 17.322767f, 17.661678f, 18.4817f, 18.726303f, 19.43805f, 19.891066f,
			20.24347f, 20.863447f
		};

		//GD.Print(song);
		//var noteTypesList = song.AsGodotDictionary()["note_types"].AsInt32Array().ToList();
		List<int> noteTypesList = Enumerable.Repeat(0, beatTimesList.Count).ToList();

		for (int x = 0; x < beatTimesList.Count; x++)
		{
			levelNotes.Add(beatTimesList[x], noteTypesList[x]);
		}
	}

	private bool DoesFolderExist(string godotPath) {
		GD.Print(ProjectSettings.GlobalizePath(godotPath) + " exists: " + Directory.Exists(ProjectSettings.GlobalizePath(godotPath)));
		return Directory.Exists(ProjectSettings.GlobalizePath(godotPath));
	}


	public static Variant GetSongFromJson(string path)
	{
		string json = "";
		try
		{	
			json = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read).GetAsText();

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
			if (ySpawnPos >= GetViewportRect().Size.Y - 100 || ySpawnPos <= 300)
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
	//	LoadSong();
		musicPlayer.StopMusic();
		RemoveAllNotes();
		index = 0;
		time = 0;
		musicPlayer.PlayMusic(MUSIC_DELAY / 1000);
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
		
		if (index < levelNotes.Keys.Count && time >= levelNotes.ElementAt(index).Key)
		{
			//currentNoteSpawnInterval = DEFAULT_NOTE_SPAWN_INTERVAL;
			switch (levelNotes[levelNotes.ElementAt(index).Key])
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
