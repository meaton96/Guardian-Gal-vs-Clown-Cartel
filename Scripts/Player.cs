using Godot;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D sprite;
	private Vector2 mouseDownPosition;
	private float mouseDownTime;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	
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

				if (timePressed < 100)
				{
					HandleClick();
				}
				else if (distance > 100)
				{
					HandleSwipe();
				}
				else
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
	}
	private void HandleLongPress()
	{
		GD.Print("Long press");
	}
	private void HandleSwipe()
	{
		GD.Print("Swipe");
	}
}
