using Godot;
using System;

public partial class SettingsMenu : Control
{
	private Button backButton;
	private Control mainMenu;

	private const float SPEED = 0.1f;
	private float velocity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		backButton = GetNode<Button>("BackButton");
		backButton.Pressed += CloseSettings;

		mainMenu = GetNode<Control>("../MainMenu");

		velocity = -SPEED;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		backButton.Position += new Vector2(0, velocity);

		if (backButton.Position.Y >= 20)
		{
			velocity = -SPEED;
		}
		if (backButton.Position.Y <= 0)
		{
			velocity = SPEED;
		}
	}

	private void CloseSettings()
	{
		this.Visible = false;
	}
}