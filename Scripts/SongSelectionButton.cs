using Godot;
using System;

public partial class SongSelectionButton : Button
{
	private int index = -1;
	public int Index=> index;
	
	string songPath;

	bool isSelected = false;

	// public SongSelectionButton(int index, string songPath)
	// {
	// 	this.index = index;
	// 	this.songPath = songPath;
	// 	Text = System.IO.Path.GetFileNameWithoutExtension(songPath);
		
	// }
	public void Init(int index, string songPath)
	{
		this.index = index;
		this.songPath = songPath;
		Text = System.IO.Path.GetFileNameWithoutExtension(songPath);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void ToggleSelection()
	{
		isSelected = !isSelected;
	}

}
