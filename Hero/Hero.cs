using Godot;
using System;

public partial class Hero : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float MovementSpeed = 40.0f;
	public const int Hp = 80;
	public const int MaxHp = 80;
	public Vector2 LastMovement = Vector2.Up;
	public const int Time = 0;
	public const int Experience = 0;
	public const int ExperienceLevel = 1;
	public const int CollectedExperience = 0;
	
	
	private Sprite2D Sprite;
	private Timer WalkTimer;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("HeroSprite");
		WalkTimer = GetNode<Timer>("WalkTimer");

	}

	public override void _PhysicsProcess(double delta)
	{
		movement();
	}
	private void movementold(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void movement()
	{
		var x_mov = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		var y_mov = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		var mov = new Vector2(x_mov, y_mov);
		if (mov.X > 0)
			Sprite.FlipH = true;
		else if (mov.X < 0)
			Sprite.FlipH = false;

		if (mov != Vector2.Zero)
		{
			LastMovement = mov;
			if (WalkTimer.IsStopped())
			{
				if (Sprite.Frame >= Sprite.Hframes - 1)
					Sprite.Frame = 0;
				else
					Sprite.Frame += 1;
				WalkTimer.Start();
			}
		}

		Velocity = mov.Normalized() * MovementSpeed;
		MoveAndSlide();
	}
}
