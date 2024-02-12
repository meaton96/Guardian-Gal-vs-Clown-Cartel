using Godot;

public partial class Note : Sprite2D
{
	// References
	private Line lineDetector;

	// Fields
	public bool active; // If true, note is moving across scene, else it is wating in the pool
	public float rightBound;
	public float leftBound;
	protected float speed = 10.0f;

	protected Color baseColor;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lineDetector = GetNode<Line>("../../Line");
		active = false;

		baseColor = Modulate;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Update bound locations
		rightBound = GlobalPosition.X + (XSize() * 1/2);
		leftBound = GlobalPosition.X - (XSize() * 1/2);

		if (CheckNoteHit())
		{
			Modulate = new Color(1, 1, 1);
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
			if (GlobalPosition.X > 1200)
			{
				active = false;
				GlobalPosition = new Vector2(-100, 300);
			}
		}
    }


	/// <summary>
	/// Checks if the note is touching the line detector at all
	/// </summary>
	/// <returns> True if a detection is found, false otherwise </returns>
	public bool CheckNoteHit()
	{
		// If note is active and AABB (but only on x-axis) is true
		if (active && 
			rightBound >= (lineDetector.GlobalPosition.X - (lineDetector.Size.X * 1/2)) && 
			leftBound <= (lineDetector.GlobalPosition.X + (lineDetector.Size.X * 1/2)))
		{
			return true;
		}
		return false;
	}

	/// <summary>
	/// Gets the X size (width) of the note
	/// </summary>
	/// <returns> Width/XSize of note </returns>
	private float XSize()
	{
		return Texture.GetSize().X * Scale.X;
	}
}