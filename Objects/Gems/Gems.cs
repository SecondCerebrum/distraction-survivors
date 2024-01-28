using System;
using Godot;

public partial class Gems : Area2D
{
	private AnimatedSprite2D _coin;
	private CollisionShape2D _collision;
	private int _speed = -1;

	private Sprite2D _sprite;

	public Node2D Target;
	public string Type = "red";

	public int Value = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("loot");
		_sprite = GetNode<Sprite2D>("Sprite");
		_collision = GetNode<CollisionShape2D>("CollisionShape");
		_coin = GetNode<AnimatedSprite2D>("Coin");
		if (Value < 5)
		{
			_sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_green.png");
			_sprite.Visible = true;
			_coin.Visible = false;
			Type = "green";
		}
		else if (Value < 15)
		{
			_sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_blue.png");
			_sprite.Visible = true;
			_coin.Visible = false;
			Type = "blue";
		}
		else
		{
			_sprite.Visible = false;
			_coin.Visible = true;
			Type = "red";
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Target != null)
		{
			var random = new Random();
			var direction = 1;
			if (GameState.Bought.Contains(SkillItemName.CoinMagnet)) direction = -1;

			// if (ab == 1)
			// GlobalPosition = GlobalPosition.DirectionTo(Target.Position);
			// else
			GlobalPosition = GlobalPosition.MoveToward(Target.Position, _speed * direction);

			_speed += Convert.ToInt16(2 * delta);
		}
	}

	public void Collect()
	{
		_collision.CallDeferred("set", "disabled", true);
		_sprite.Visible = false;
		_coin.Visible = false;
	}

	public void OnSndCollectedFinished()
	{
		QueueFree();
	}
}
