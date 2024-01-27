using Godot;
using System;

public partial class CollectCoinsWindow : Window
{
	private int _amount = 10;
	private AnimatedSprite2D _coin1;
	private AnimatedSprite2D _coin2;
	private ulong _lastMove;
	private ulong _showTime;
	private ulong _keepDuration = 3000;
	private Random _random;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coin1 = GetNode<AnimatedSprite2D>("VBoxContainer/Collect/Coin1");
		_coin2 = GetNode<AnimatedSprite2D>("VBoxContainer/Collect/Coin2");
		_coin1.Play();
		_coin2.Play();
		_random = new Random();
		_showTime = Time.GetTicksMsec();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var curTime = Time.GetTicksMsec();
		if (_showTime + _keepDuration < curTime) Hide();
		if (_lastMove + 100 < curTime)
		{
			int newX = Position.X + _random.Next(-10, 10);
			int newY = Position.Y + _random.Next(-10, 10);
			Position = new Vector2I(newX, newY);
			_lastMove = curTime;
		}
	}

	private void _on_collect()
	{
		GameState.Coins += _amount;
		Hide();
	}

	private void _on_close_requested()
	{
		Hide();
	}

	private void _on_visibility_changed()
	{
		if (Visible)
		{
			_showTime = Time.GetTicksMsec();
		}
	}
}
