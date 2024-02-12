using Godot;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D sprite;
	private Vector2 mouseDownPosition;

	private const float SWIPE_THRESHOLD = 100;
	private const float LONG_PRESS_THRESHOLD = 200;

	private Line lineDetector;
	private Interface ui;
	private float mouseDownTime;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		lineDetector = GetNode<Line>("Line");
		ui = GetNode<Interface>("UserInterface");
		//GD.Print(lineDetector);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left)
			{
				mouseDownPosition = mouseEvent.Position;
				mouseDownTime = Time.GetTicksMsec();

				// Need this to trigger on click but NOT on release	
				HandleClick();
			}
		}
		else if (@event is InputEventMouseButton mouseEventUp && !mouseEventUp.Pressed)
		{

			if (mouseEventUp.ButtonIndex == MouseButton.Left)
			{
				var mouseUpPosition = mouseEventUp.Position;
				var distance = mouseDownPosition.DistanceTo(mouseUpPosition);
				var timePressed = Time.GetTicksMsec() - mouseDownTime;

				//do something with this info
				//timePressed under ~100ms is a click
				//distance is how far the mouse was dragged for swipe gestures
				//there is a built in swipe gesture in Godot, but this is ok for now
				//also built in info about input from touch screens on mobile 
				//example:

				if (distance > SWIPE_THRESHOLD)
				{
					HandleSwipe();
				}
				else if (timePressed > LONG_PRESS_THRESHOLD)
				{
					HandleLongPress();
				}


				//	GD.Print($"Left button was released at {mouseEventUp.Position} distance: {distance} time: {timePressed}");
			}
		}
	}

	private void HandleClick()
	{
		GD.Print("Click");
		if (lineDetector.CheckTap())
		{
			ui.DisplayHit();
		}
		else {
			ui.DisplayMiss();
		
		}
	}
	private void HandleLongPress()
	{
		GD.Print("Long press");
		if (lineDetector.CheckHold())
		{
			ui.DisplayHit();
		}
		else {
			ui.DisplayMiss();
		}
	}
	private void HandleSwipe()
	{
		GD.Print("Swipe");
		if (lineDetector.CheckSwipe())
		{
			ui.DisplayHit();
		}
		else {
			ui.DisplayMiss();
		}
	}
}
