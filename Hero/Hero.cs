using System;
using System.Collections.Generic;
using Godot;

public partial class Hero : CharacterBody2D
{
	public const int CollectedExperience = 0;
	private Sprite2D _axeSprite;
	private Label _coinsValue;

	private Area2D _collecting;
	private Area2D _gathering;
	private TextureProgressBar _healthBar;

	private int _kills;

	private Sprite2D _sprite;
	private int _time;

	private Label _timeLabel;

	private Timer _walkTimer;

	// public int Time = 0;
	// public int Experience = 0;
	// public int ExperienceLevel = 1;
	public int Armor = 0;

	public Dictionary<string, int> CollectedGems = new()
	{
		{ "red", 0 },
		{ "green", 0 },
		{ "blue", 0 }
	};

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public int Hp = 80;
	public int HpMax = 80;

	public Vector2 LastMovement = Vector2.Up;

	public float MovementSpeed = 240.0f;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("HeroSprite");
		_walkTimer = GetNode<Timer>("WalkTimer");
		_gathering = GetNode<Area2D>("Gathering");
		_collecting = GetNode<Area2D>("Collecting");
		_timeLabel = GetNode<Label>("GUILayer/GUI/TimeLabel");
		_healthBar = GetNode<TextureProgressBar>("GUILayer/GUI/HealthBar");
		_coinsValue = GetNode<Label>("GUILayer/GUI/CoinsLabel/CoinsValue");
		_axeSprite = GetNode<Sprite2D>("Axe");

		_gathering.Connect("area_entered", new Callable(this, nameof(OnGatheringAreaEntered)));
		_collecting.Connect("area_entered", new Callable(this, nameof(OnCollectingAreaEntered)));

		Run();
	}

	public void Run()
	{
		_healthBar.MaxValue = HpMax;
		_healthBar.Value = Hp;
		_kills = 0;

		CollectedGems = new Dictionary<string, int>
		{
			{ "red", 0 },
			{ "green", 0 },
			{ "blue", 0 }
		};
		LastMovement = Vector2.Up;

		MovementSpeed = 240.0f;

		// var tween = CreateTween();
		// tween.TweenProperty(_axeSprite, "rotation", 10, 2);
		// tween.TweenProperty(_axeSprite, "rotation", 45, 2);
		// tween.Play();

		GetTree().Paused = false;
	}

	private void OnGatheringAreaEntered(Node area)
	{
		GD.Print(area);
		if (area.IsInGroup("loot"))
		{
			GD.Print("OnGatheringAreaEntered", " ", area);
			area.Set("Target", this);
		}
	}

	private void OnCollectingAreaEntered(Node area)
	{
		if (area.IsInGroup("loot"))
		{
			var isStuffed = GameState.Bought.Contains(SkillItemName.Stuffed);
			var isFrackingBlue = GameState.Bought.Contains(SkillItemName.FrackingBlue);
			if ((area as Gems)?.Type == "green" && isStuffed)
			{
				area.Call("Collect");
				CollectedGems["green"] += 1;
			}
			else if ((area as Gems)?.Type == "blue" && isFrackingBlue)
			{
				area.Call("Collect");
				CollectedGems["blue"] += 1;
			}
			else if ((area as Gems)?.Type == "red")
			{
				area.Call("Collect");
				CollectedGems["red"] += 1;
				_coinsValue.Text = $"{CollectedGems["red"]}";
				GameState.Coins += 1;
			}
		}
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
			_sprite.FlipH = true;
		else if (mov.X < 0)
			_sprite.FlipH = false;

		if (mov != Vector2.Zero)
		{
			LastMovement = mov;
			if (_walkTimer.IsStopped())
			{
				if (_sprite.Frame >= _sprite.Hframes - 1)
					_sprite.Frame = 0;
				else
					_sprite.Frame += 1;
				_walkTimer.Start();
			}
		}

		Velocity = mov.Normalized() * MovementSpeed;
		MoveAndSlide();
	}

	private void _on_hurt_box_2_hurt(long damage, double angle, double kockbackAmount)
	{
		Hp -= Convert.ToInt32(Math.Clamp(damage - Armor, 1.0, 999.0));
		_healthBar.MaxValue = HpMax;
		_healthBar.Value = Hp;
		if (Hp <= 0) death();
	}

	private void death()
	{
		DisplaySummaryPanel();
	}

	private void DisplaySummaryPanel()
	{
		GetTree().Paused = true;
		var world = GetTree().Root.GetNode("World") as World;
		world.OpenRoundSummary(CollectedGems["red"], CollectedGems["blue"], _time);
	}

	private void _on_enemy_spawner_change_time(int argtime)
	{
		_time = argtime;
		var get_m = Convert.ToInt32(_time / 60.0);
		var get_s = _time % 60;
		var min_str = $"{get_m}";
		var sec_str = $"{get_s}";
		if (get_m < 10)
			min_str = $"0{get_m}";
		if (get_s < 10)
			sec_str = $"0{get_s}";
		_timeLabel.Text = $"{min_str}:{sec_str}";
	}

	private void _on_enemy_spawner_game_over()
	{
		DisplaySummaryPanel();
	}
}
