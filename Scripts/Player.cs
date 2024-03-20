using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Godot;

public partial class Player : CharacterBody2D
{
	private const bool DEBUG_ENABLED = true;
	private AnimatedSprite2D sprite;
	private Vector2 mouseDownPosition;

	private const float SWIPE_THRESHOLD = 100;
	private const float LONG_PRESS_THRESHOLD = 200;

	private Line lineDetector;
	private PointHandling pointController;
	private Interface ui;
	private float mouseDownTime;
	private string platform;

	public bool IsLongPressing = false;
	public Vector2 TouchPosition;

	private bool dragged = false;

	private bool mouseDown;

	//public bool IsPaused = false;

	private float timePressed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		lineDetector = GetNode<Line>("../Line");
		platform = OS.GetName();
		pointController = GetNode<PointHandling>("../PointController");
		ui = GetNode<Interface>("../UserInterface/UserInterface");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsLongPressing)
		{
			GD.Print($"({TouchPosition.X}, {TouchPosition.Y})");
		}
	}

	public override void _Input(InputEvent @event)
	{
		//something touch screen related
		if (platform == "Android")
			HandleTouchInput(@event);
		else
			HandleMouseInputNew(@event);

		if (DEBUG_ENABLED)
		{
			HandleKeyboardInput(@event);
		}

	}

	private void HandleKeyboardInput(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.Pressed)
			{
				if (keyEvent.Keycode == Key.Escape)
				{
					ui.ClearInput();
				}
			}
		}
	}

	private void HandleTouchInput(InputEvent @event)
	{
		// First check if there is a note to detect at all
		if (lineDetector.CheckNoteDetection())
		{
			//touch input
			if (@event is InputEventScreenTouch screenTouch)
			{
				//ui.SetFeedback("screen touch");
				//press down
				if (screenTouch.Pressed)
				{
					//get the position and time of the press
					mouseDown = true;
					mouseDownPosition = screenTouch.Position;
					mouseDownTime = Time.GetTicksMsec();
				}
				//release press
				else
				{
					mouseDown = false;
					timePressed = Time.GetTicksMsec() - mouseDownTime;
					IsLongPressing = false;
					if (timePressed > LONG_PRESS_THRESHOLD)
					{
						//HoldNote noteType = new HoldNote();
						HandleNoteDetection(typeof(HoldNote));
					}
					else
					{
						if (!dragged)
						{
							//RegNote noteType = new RegNote();
							HandleNoteDetection(typeof(RegNote));
						}
					}
					dragged = false;
				}
			}
			else if (@event is InputEventScreenDrag screenDrag)
			{
				//get the duration of press
				timePressed = Time.GetTicksMsec() - mouseDownTime;
				//need to differentiate between drag and swipe
				//if swipe is long enough its a long press and drag for hold note
				//ui.SetFeedback("screen drag");
				if (timePressed > LONG_PRESS_THRESHOLD)
				{
					//long press + screen drag = hold note
					//set the position of the touch
					IsLongPressing = true;
					TouchPosition = screenDrag.Position;

				}
				else if (screenDrag.Relative.Length() > SWIPE_THRESHOLD)
				{
					if (!dragged)
					{
						dragged = true;

						// Swipe note detection
						//SwipeNote noteType = new SwipeNote();
						HandleNoteDetection(typeof(SwipeNote));
					}

				}
			}
		}
		else
		{
			//GD.Print("what");
			pointController.Miss();
		}
	}
	private void HandleMouseInputNew(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			//left mouse down
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{

				if (lineDetector.CheckNoteDetection())
				{
					//check for regular note
					HandleNoteDetection(typeof(RegNote));
				}
				else
				{
					//lose points?
					//clicked when no note was present
				}
			}

		}
		else if (@event is InputEventMouseMotion mouseMotionEvent)
		{
			//if the left mouse button is pressed
			if (mouseMotionEvent.ButtonMask.HasFlag(MouseButtonMask.Left))
			{

				if (lineDetector.CheckNoteDetection())
				{
					if (lineDetector.GetActivteNoteType() == typeof(SwipeNote))
						HandleNoteDetection(typeof(SwipeNote));
					else
						HandleNoteDetection(typeof(HoldNote));
				}
			}

		}
	}

	// private void HandleMouseInput(InputEvent @event)
	// {
	// 	if (lineDetector.CheckNoteDetection())
	// 	{
	// 		if (@event is InputEventMouseButton mouseEvent)
	// 		{
	// 			//ui.SetFeedback("mouse down");
	// 			if (mouseEvent.ButtonIndex != MouseButton.Left) return;

	// 			//mouse down
	// 			if (mouseEvent.Pressed && !mouseDown)
	// 			{
	// 				mouseDown = true;
	// 				mouseDownPosition = mouseEvent.Position;
	// 				mouseDownTime = Time.GetTicksMsec();

	// 			}
	// 			//mouse up
	// 			else if (!mouseEvent.Pressed && mouseDown)
	// 			{
	// 				mouseDown = false;
	// 				//ui.SetFeedback("mouse up");
	// 				var mouseUpPosition = mouseEvent.Position;
	// 				var distance = mouseDownPosition.DistanceTo(mouseUpPosition);
	// 				timePressed = Time.GetTicksMsec() - mouseDownTime;


	// 				if (timePressed > LONG_PRESS_THRESHOLD)
	// 				{
	// 					//HoldNote noteType = new HoldNote();
	// 					HandleNoteDetection(typeof(HoldNote));

	// 				}
	// 				else if (distance > SWIPE_THRESHOLD)
	// 				{
	// 					//SwipeNote noteType = new SwipeNote();
	// 					HandleNoteDetection(typeof(SwipeNote));
	// 				}
	// 				else
	// 				{
	// 					//RegNote noteType = new RegNote();
	// 					HandleNoteDetection(typeof(RegNote));
	// 				}
	// 			}
	// 		}
	// 		else if (@event is InputEventMouseMotion mouseMotion)
	// 		{
	// 			//mouse motion
	// 			//ui.SetFeedback("mouse motion");
	// 			if (!mouseMotion.ButtonMask.HasFlag(MouseButtonMask.Left)) return;
	// 			timePressed = Time.GetTicksMsec() - mouseDownTime;
	// 			//if the time pressed is greater than the long press threshold


	// 			TouchPosition = mouseMotion.Position;

	// 			if (timePressed > LONG_PRESS_THRESHOLD)
	// 			{
	// 				IsLongPressing = true;
	// 			}
	// 		}
	// 	}
	// 	else
	// 	{
	// 		pointController.Miss();
	// 	}
	// }
	private void HandleNoteDetection(Type noteType)
	{
		//	GD.Print("Click");
		ui.AddInput("tap");
		if (lineDetector.hitNote.GetType() == noteType)
		{
			int scoreCategory = lineDetector.hitNote.DisableNote(true);
			switch (scoreCategory)
			{
				case 0:
					pointController.Perfect();
					break;
				case 1:
					pointController.Great();
					break;
				case 2:
					pointController.Good();
					break;
				case 3:
					pointController.OK();
					break;
				default:
					pointController.Miss();
					break;

			}
			//ui.DisplayHit();
		}
		else
		{
			//ui.DisplayMiss();

		}
	}
	// private void HandleLongPress()
	// {
	// 	//	GD.Print("Long press");
	// 	ui.AddInput("Long press");
	// 	if (lineDetector.CheckNotes())
	// 	{
	// 		lineDetector.hitNote.DisableNote();
	// 		//ui.DisplayHit();
	// 	}
	// 	else
	// 	{
	// 		//ui.DisplayMiss();
	// 	}
	// }
	// private void HandleSwipe()
	// {
	// 	//	GD.Print("Swipe");
	// 	ui.AddInput("Swipe");
	// 	if (lineDetector.CheckNotes())
	// 	{
	// 		lineDetector.hitNote.DisableNote();
	// 		//ui.DisplayHit();
	// 	}
	// 	else
	// 	{
	// 		//ui.DisplayMiss();
	// 	}
	// }
}
