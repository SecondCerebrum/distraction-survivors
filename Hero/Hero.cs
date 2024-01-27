using System;
using Godot;

public partial class Hero : CharacterBody2D
{
	public const int CollectedExperience = 0;

	// public int Time = 0;
	// public int Experience = 0;
	// public int ExperienceLevel = 1;
	public int Armor = 0;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public int Hp = 80;
	public int HpMax = 80;

	// private Area2D HurtBox;
	public Vector2 LastMovement = Vector2.Up;
	// [Signal]
	// public delegate void HurtEventHandler();

	public float MovementSpeed = 240.0f;

	private Sprite2D Sprite;

	private Timer WalkTimer;
	// deathPanel.visible = true

	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("HeroSprite");
		WalkTimer = GetNode<Timer>("WalkTimer");

		// Hurt
		// var HurtBox = GetTree().Root.GetNode<Area2D>("World/Hero/HurtBox2");
		// var HurtBox = GetNode<Area2D>("HurtBox2");
		// GD.Print("HurtBox", " ", HurtBox);
		// HurtBox.Set("Hurt", OnHurt);
		// HurtBox.Connect("Hurt", new Callable(this, nameof(OnHurt)));
	}

	public override void _PhysicsProcess(double delta)
	{
		movement();
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

	// public void OnHurt()
	// {
	// 	GD.Print("OnHurt");
	// 	// EmitSignal("Hurt");
	// }
	private void _on_hurt_box_2_hurt(long damage, double angle, double kockbackAmount)
	{
		Hp -= Convert.ToInt32(Math.Clamp(damage - Armor, 1.0, 999.0));
		// healthBar.max_value = maxhp
		// healthBar.value = hp
		if (Hp <= 0) death();
	}

	private void death()
	{
		EmitSignal("playerdeath");
		GetTree().Paused = true;
	}
	// var tween = deathPanel.create_tween()
	// tween.tween_property(deathPanel,"position",Vector2(220,50),3.0).set_trans(Tween.TRANS_QUINT).set_ease(Tween.EASE_OUT)
	// tween.play()
	// if time >= 300:
	// lblResult.text = "You Win"
	// sndVictory.play()
	// else:
	// lblResult.text = "You Lose"
	// sndLose.play()
}
