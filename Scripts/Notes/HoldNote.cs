using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class HoldNote : Note
{
	// Fields


	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//xSpawnPos = -320;
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);

	}
	public override void EnableNote(float spawnPosY = 300)
	{

		active = true;
		GlobalPosition = new Vector2(xSpawnPos - 300, ySpawnPos);
		
	}

    protected override void ConstantHitEffect()
    {
        base.ConstantHitEffect();
		
		// Do point stuff
    }
}
