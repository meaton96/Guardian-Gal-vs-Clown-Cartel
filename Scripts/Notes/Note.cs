using Godot;
using System;

public partial class Note : Sprite2D
{
	// Fields
	public bool active; // If true, note is moving across scene, else it is wating in the pool
	private float speed = 10.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		active = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print("Stopping");
	}

    public override void _PhysicsProcess(double delta)
    {
		// Check if the note is active
		if (active)
		{
			// Direction (1 on the x) * speed, y does not change
			Vector2 move = new Vector2(1.0f * speed, 0.0f);

			// Add to global position
			GlobalPosition += move;

			// print information for testing
			//GD.Print("Move " + Name + " by " + move + " to " + GlobalPosition);
			if (GlobalPosition.X > 1200)
			{
				GD.Print("Stopping");
				active = false;
				GlobalPosition = new Vector2(-100, 300);
			}
		}
    }
}
