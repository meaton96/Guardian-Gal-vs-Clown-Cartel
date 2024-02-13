using System.Reflection.Metadata;
using Godot;

public partial class Player : CharacterBody2D
{
	private const bool DEBUG_ENABLED = true;
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
		//something touch screen related
		HandleTouchInput(@event);
		//something mouse button related
		HandleMouseInput(@event);

		if (DEBUG_ENABLED) {
			HandleKeyboardInput(@event);	
		}

	}
	private void HandleKeyboardInput(InputEvent @event) {
		if (@event is InputEventKey keyEvent) {
			if (keyEvent.Pressed) {
				if (keyEvent.Keycode == Key.Escape) {
					ui.ClearInput();
				}
			}
		}
	}
	
	private void HandleTouchInput(InputEvent @event)
	{
		if (@event is InputEventScreenTouch screenTouch)
		{
			ui.SetFeedback("screen touch");
			if (screenTouch.Pressed)
			{
				mouseDownPosition = screenTouch.Position;
				mouseDownTime = Time.GetTicksMsec();
			}
			else
			{
				var timePressed = Time.GetTicksMsec() - mouseDownTime;

				if (timePressed > LONG_PRESS_THRESHOLD)
				{
					HandleLongPress();
				}
				else
				{
					HandleClick();
				}
			}
		}
		else if (@event is InputEventScreenDrag screenDrag)
		{
			
			ui.SetFeedback("screen drag");
			if (screenDrag.Relative.Length() > SWIPE_THRESHOLD)
			{
				HandleSwipe();
			}
		}
	}
	private void HandleMouseInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			ui.SetFeedback("mouse down");
			if (mouseEvent.ButtonIndex != MouseButton.Left) return;
			//mouse down
			if (mouseEvent.Pressed)
			{

				mouseDownPosition = mouseEvent.Position;
				mouseDownTime = Time.GetTicksMsec();

			}
			//mouse up
			else if (!mouseEvent.Pressed)
			{
				ui.SetFeedback("mouse up");
				var mouseUpPosition = mouseEvent.Position;
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
				else
				{
					HandleClick();
				}
			}
		}
	}
	private void HandleClick()
	{
		//	GD.Print("Click");
		ui.AddInput("Click");
		if (lineDetector.CheckTap())
		{
			lineDetector.hitNote.DisableNote();
			//ui.DisplayHit();
		}
		else
		{
			//ui.DisplayMiss();

		}
	}
	private void HandleLongPress()
	{
		//	GD.Print("Long press");
		ui.AddInput("Long press");
		if (lineDetector.CheckHold())
		{
			lineDetector.hitNote.DisableNote();
			//ui.DisplayHit();
		}
		else
		{
			//ui.DisplayMiss();
		}
	}
	private void HandleSwipe()
	{
		//	GD.Print("Swipe");
		ui.AddInput("Swipe");
		if (lineDetector.CheckSwipe())
		{
			lineDetector.hitNote.DisableNote();
			//ui.DisplayHit();
		}
		else
		{
			//ui.DisplayMiss();
		}
	}
}
