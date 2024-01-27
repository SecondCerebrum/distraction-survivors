using Godot;
using System;

public partial class HitBox : Area2D
{
	public enum HitBoxTypes : ushort { Cooldown = 0, HitOnce = 1, DisableHitBox = 2  };
	[Export] public HitBoxTypes HitBoxType = HitBoxTypes.Cooldown;

	CollisionShape2D collision;
	Timer DisableTimer;
	public override void _Ready()
	{
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		DisableTimer = GetNode<Timer>("DisableTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
