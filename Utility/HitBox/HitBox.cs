using Godot;

public partial class HitBox : Area2D
{
	public enum HitBoxTypes : ushort
	{
		Cooldown = 0,
		HitOnce = 1,
		DisableHitBox = 2
	}

	private CollisionShape2D _collision;
	private Timer _disableTimer;

	public int Damage = 1;
	[Export] public HitBoxTypes HitBoxType = HitBoxTypes.Cooldown;

	public override void _Ready()
	{
		base._Ready();
		_collision = GetNode<CollisionShape2D>("CollisionShape2D");
		_disableTimer = GetNode<Timer>("DisableTimer");

		AddToGroup("attack");

		// CollisionLayer = 0;
		// CollisionMask = 0;

		_disableTimer.WaitTime = 0.5f;
		_disableTimer.OneShot = true;

		_disableTimer.Connect("timeout", new Callable(this, nameof(OnDisableHitBoxTimerTimeout)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void TempDisable()
	{
		_collision.CallDeferred("set", "disabled", true);
		_disableTimer.Start();
	}

	private void OnDisableHitBoxTimerTimeout()
	{
		_collision.CallDeferred("set", "disabled", false);
	}
}
