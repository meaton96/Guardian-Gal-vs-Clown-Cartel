using System;
using System.Collections.Generic;
using Godot;

public partial class Note : Sprite2D
{
	// References
	private Line lineDetector;		

	private readonly List<float> hitBreakpoints = new() {
		0.2f,
		0.5f,
		0.75f,
		1.01f
	};


	// Fields
	public bool active; // If true, note is moving across scene, else it is wating in the pool
	public float rightBound;
	public float leftBound;
	protected float speed = 4.0f;
	protected Color baseColor;
	protected Control bounds;
	private float timer = 0;

	// Change in each note definition, default is middle of screen
	protected float ySpawnPos = 300;
	protected float xSpawnPos = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lineDetector = GetNode<Line>("../../Line");
		//active = false;
		bounds = GetNode<Control>("Bounds");
		baseColor = Modulate;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	//	GD.Print("Note Pos: " + GlobalPosition);

		// Update bound locations
		rightBound = GlobalPosition.X + GetXSize();
		leftBound = GlobalPosition.X - GetXSize();

		timer += (float)delta;
		// Hit effect
		if (CheckNoteHit())
		{
		//	GD.Print(timer);
			Modulate = new Color(0, 1, 0);
			ConstantHitEffect();
		}
		else
		{
			Modulate = baseColor;
		}
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
			if (GlobalPosition.X > GetViewportRect().Size.X + 200)
			{
				DisableNote();
			}
		}
	}

	protected virtual void ConstantHitEffect()
	{
		// Does nothing, will be overridden in child classes
	}


	/// <summary>
	/// Checks if the note is touching the line detector at all
	/// </summary>
	/// <returns> True if a detection is found, false otherwise </returns>
	public virtual bool CheckNoteHit()
	{
		// If note is active and AABB (but only on x-axis) is true
		if (active &&
			rightBound >= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1 / 2)) &&
			leftBound <= (lineDetector.GlobalPosition.X + (lineDetector.Size.X * 1 / 2)))
		{
			return true;
		}
		return false;
	}

	public virtual void EnableNote(float spawnPosY = 300)
	{
		active = true;
		GlobalPosition = new Vector2(xSpawnPos, spawnPosY);
	}

	public int DisableNote(bool hit = false)
	{
		if (hit && active) {
			float xDistance = Mathf.Abs(rightBound - lineDetector.GlobalPosition.X);
			float percentOfSize = xDistance / (GetXSize() * 2);

			int index = hitBreakpoints.FindIndex(bp => percentOfSize < bp);
			active = false;
			return index >= 0 ? index : -1;

		}
		

		active = false;
		return -1;
		//GlobalPosition = new Vector2(xSpawnPos, ySpawnPos);
	}

	private float GetXSize()
	{
		return bounds.Size.X * Scale.X;
	}
}