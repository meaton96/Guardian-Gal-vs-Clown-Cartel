using Godot;

public partial class Note : Sprite2D
{
	// References
	private Line lineDetector;																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																				
	private PointHandling points;

	// Fields
	public bool active; // If true, note is moving across scene, else it is wating in the pool
	public float rightBound;
	public float leftBound;
	protected float speed = 10.0f;
	protected Color baseColor;
	protected Control bounds;

	// Change in each note definition, default is middle of screen
	protected float ySpawnPos = 300;
	protected float xSpawnPos = 400;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lineDetector = GetNode<Line>("../../Line");
		active = false;
		bounds = GetNode<Control>("Bounds");
		points = GetNode<PointHandling>("../../PointController");

		baseColor = Modulate;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Update bound locations
		rightBound = GlobalPosition.X + GetXSize();
		leftBound = GlobalPosition.X - GetXSize();

		// Hit effect
		if (CheckNoteHit())
		{
			Modulate = new Color(0, 1, 0);
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
			points.HandleScore(this, lineDetector.GlobalPosition.X, lineDetector.Size.X);
			return true;
		}
		return false;
	}

	public void EnableNote()
	{
		active = true;
		GlobalPosition = new Vector2(xSpawnPos, ySpawnPos);
	}

	public void DisableNote()
	{
		active = false;
		GlobalPosition = new Vector2(xSpawnPos, ySpawnPos);
	}

	private float GetXSize()
	{
		return bounds.Size.X * Scale.X;
	}
}