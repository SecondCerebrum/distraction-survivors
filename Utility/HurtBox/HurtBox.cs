using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class HurtBox : Area2D
{
	[Signal]
	public delegate void HurtEventHandler(int damage, float angle, float kockbackAmount);

	public enum HurtBoxTypes : ushort
	{
		Cooldown = 0,
		HitOnce = 1,
		DisableHurtBox = 2
	}

	private readonly List<CollisionObject2D> _hitOnceArray = new();

	private CollisionShape2D _collision;

	private int _damage = 1;
	private Timer _disableTimer;
	public HurtBoxTypes HurtBoxType = HurtBoxTypes.Cooldown;

	public override void _Ready()
	{
		base._Ready();
		_collision = GetNode<CollisionShape2D>("CollisionShape2D");
		_disableTimer = GetNode<Timer>("DisableTimer");

		// CollisionLayer = 0;
		// CollisionMask = 0;

		_disableTimer.WaitTime = 0.5f;
		_disableTimer.OneShot = true;

		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
		_disableTimer.Connect("timeout", new Callable(this, nameof(OnDisableTimerTimeout)));
	}

	private void OnAreaEntered(CollisionObject2D area)
	{
		var parent = area.GetParent();
		var hasSameElements = GetParent().GetGroups().Intersect(parent.GetGroups()).Any();
		// GD.Print(hasSameElements);
		// GD.Print(, " ", area.GetGroups());
		if (hasSameElements) return;
		if (area.IsInGroup("attack"))
			if (area.Get("Damage").VariantType != Variant.Type.Nil)
			{
				switch (HurtBoxType)
				{
					case HurtBoxTypes.Cooldown:
						_collision.CallDeferred("set", "disabled", true);
						_disableTimer.Start();

						break;
					case HurtBoxTypes.HitOnce:
						if (_hitOnceArray.Contains(area) == false)
						{
							_hitOnceArray.Append(area);
							if (area.HasSignal("RemoveFromArray"))
								if (area.IsConnected("RemoveFromArray", new Callable(this, "RemoveFromList")) == false)
									area.Connect("RemoveFromArray", new Callable(this, "RemoveFromList"));
						}
						else
						{
							return;
						}

						GD.Print("HurtBoxTypes.HitOnce", HurtBoxTypes.HitOnce);

						break;
					case HurtBoxTypes.DisableHurtBox:
						if (area.HasMethod("TempDisable")) area.Call("TempDisable");
						break;
				}

				var damage = area.Get("damage");
				Variant angle = 0;
				Variant kockbackAmount = 1;
				if (area.Get("angle").VariantType != Variant.Type.Nil) angle = area.Get("angle");
				if (area.Get("kockbackAmount").VariantType != Variant.Type.Nil)
					kockbackAmount = area.Get("kockbackAmount");

				EmitSignal(SignalName.Hurt, damage, angle, kockbackAmount);
				if (area.HasMethod("EnemyHit")) area.Call("EnemyHit", 1);
			}
	}

	private void RemoveFromList(CollisionObject2D area)
	{
		if (_hitOnceArray.Contains(area))
			_hitOnceArray.Remove(area);
	}

	private void TempDisable()
	{
		_collision.CallDeferred("set", "disabled", true);
		_disableTimer.Start();
	}

	private void OnDisableTimerTimeout()
	{
		_collision.CallDeferred("set", "disabled", false);
	}
}
