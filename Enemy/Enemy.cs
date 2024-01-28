using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
	private readonly float _kockbackRecovery = 3.5f;

	private int _experience = 1;

	private CharacterBody2D _hero;

	private int _kockback;
	private PackedScene _loot = GD.Load<PackedScene>("res://Objects/Gems/Gems.tscn");
	private Node _lootBase;

	private float _movementSpeed = 120.0f;
	private Sprite2D _sprite;
	public int Demage = 1;

	public int Hp = 2;

	public void OnHurtBoxHurt(int damage, int angle, int kockbackAmount)
	{
		Hp -= damage;
		_kockback = angle * kockbackAmount;
		if (Hp <= 0) death();
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hero = GetTree().Root.GetNode<CharacterBody2D>("World/Hero");
		_sprite = GetNode<Sprite2D>("Sprite");
		_lootBase = GetTree().Root.GetNode<Node>("World"); //GetFirstNodeInGroup("loot");

		AddToGroup("enemy");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		// _kockback = _kockback.MoveToward(Vector2.Zero, _kockbackRecovery);
		var direction = Position.DirectionTo(_hero.GlobalPosition);
		Velocity = direction * _movementSpeed;
		// Velocity += _kockback;
		MoveAndSlide();

		if (direction.X > 0.1) _sprite.FlipH = true;
		else if (direction.X < -0.1) _sprite.FlipH = false;
	}

	public void death()
	{
		// EmitSignal("RemoveFromArray", this);
		// animation of death
		// add loot
		var loot = _loot.Instantiate() as Gems;
		loot.GlobalPosition = GlobalPosition;
		var random = new Random();
		loot.Value = random.Next(1, 30);
		_lootBase.CallDeferred("add_child", loot);
		QueueFree();
	}

	private void _on_hurt_box_2_hurt(int damage, int angle, int kockbackAmount)
	{
		Hp -= 1;
		if (Hp <= 0) death();
	}
}
