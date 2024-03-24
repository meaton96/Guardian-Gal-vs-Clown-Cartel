using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class SongSelector : Node2D
{
	const string AUDIO_FOLDER_PATH = @"audio";
	List<SongSelectionButton> currentSongButtons;
	private int currentSongIndex = -1;

	private SongSelectionButton currentSongButton;
	private List<string> songPaths;
	PackedScene songSelectionButtonScene;
	VBoxContainer leftColumn, rightColumn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		leftColumn = GetNode<VBoxContainer>("Columns/LeftCol");
		rightColumn = GetNode<VBoxContainer>("Columns/RightCol");
		songSelectionButtonScene = GD.Load<PackedScene>("res://Prefabs/song_selection_button.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private List<string> FindSongs()
	{
		return Directory.GetFiles(AUDIO_FOLDER_PATH, "*.json").ToList();
	}

	public void LookForNewSongs()
	{
		currentSongButtons = new();
		songPaths = FindSongs();
		songPaths.ForEach(song => GD.Print(song));
		int x = 0;
		foreach (var song in songPaths)
		{
			var songButton = songSelectionButtonScene.Instantiate() as SongSelectionButton;
			songButton.AddToGroup("songButtons");
			songButton.Init(x, song);
			if (x % 2 == 0)
			{
				leftColumn.AddChild(songButton);
			}
			else
			{
				rightColumn.AddChild(songButton);
			}
			songButton.Pressed += () => OnSongButtonPressed(x);

			x++;
		}


	}
	private void OnSongButtonPressed(int index)
	{
		currentSongIndex = currentSongButtons[index].Index;
		currentSongButton = currentSongButtons[index];
		if (currentSongIndex != -1)
		{
			SetSelectionColor(currentSongIndex);
		}
		

	}

	private void SetSelectionColor(int index) {
		
		//currentSongButtons[index].Modulate = new Color(1, 0, 0);
		currentSongButtons.ForEach(button => {
			if (button.Index != index)
			{
				ClearSelectionColor(button.Index);
			}
		});

	}
	private void ClearSelectionColor(int index) {
		//currentSongButtons[index].Modulate = new Color(1, 1, 1);
	}
}
