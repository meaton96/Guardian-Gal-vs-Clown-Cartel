using Godot;
using System;

public partial class FireGroups : Node
{
	Node2D[] fireGroups = new Node2D[3];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireGroups[0] = GetNode<Node2D>("group1");
		fireGroups[1] = GetNode<Node2D>("group2");
		fireGroups[2] = GetNode<Node2D>("group3");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void EnableGroup(int group)
	{
		//GD.Print("Enable group " + group);	
		fireGroups[group].Visible = true;
	}
	public void DisableGroup(int group)
	{
		fireGroups[group].Visible = false;
	}
	public void DisableAllGroups() {
		foreach (Node2D group in fireGroups) {
			group.Visible = false;
		}
	}
}
