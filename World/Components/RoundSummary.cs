using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RoundSummary : Window
{
	[Export] public int RoundCoins { get; set; } = 20;
	[Export] public int RoundTime { get; set; } = 40;
	[Signal] public delegate void CoinsCountUpEndEventHandler();
	[Signal] public delegate void CoinsCountDownEndEventHandler();
	[Signal] public delegate void TimeCountUpEndEventHandler();
	private AnimationPlayer _animationPlayer;
	private AudioStreamPlayer _audioStreamPlayer;
	private Button _nextRound;
	private Button _store;
	private Label _coinsLbl;
	private Label _coinsCount;
	private Label _timeCount;
	private int _countingCoins = 0;
	private int _countingTime = 0;

	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		_nextRound = GetNode<Button>("NextRound");
		_store = GetNode<Button>("Store");
		_coinsLbl = GetNode<Label>("CoinsLbl");
		_coinsCount = GetNode<Label>("CoinsCount");
		_timeCount = GetNode<Label>("TimeCount");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		GetNode<AnimatedSprite2D>("CoinsLbl/Coin").Play();
		GetNode<AnimatedSprite2D>("DiamondsLbl/Diamond").Play();
		GetNode<AnimatedSprite2D>("Store/Coin1").Play();
		GetNode<AnimatedSprite2D>("Store/Coin2").Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public async void Run()
	{
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		_coinsLbl.Show();
		_coinsCount.Show();

		var coinCounter = new Timer() { WaitTime = 0.1 };
		AddChild(coinCounter);
		coinCounter.Timeout += () => {
			_countingCoins++;
			_coinsCount.Text = _countingCoins.ToString();
			if (_countingCoins == RoundCoins)
			{
				coinCounter.Stop();
				EmitSignal(SignalName.CoinsCountUpEnd);
			}
		};
		coinCounter.Start();
		await ToSignal(this, SignalName.CoinsCountUpEnd);
		GD.Print("count end");

		if (!GameState.Bought.Contains(SkillItemName.CoinCollector))
		{
			GetNode<Label>("NoSkill").Show();
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

			coinCounter = new Timer() { WaitTime = 0.05 };
			AddChild(coinCounter);
			coinCounter.Timeout += () => {
				_countingCoins--;
				_coinsCount.Text = _countingCoins.ToString();
				if (_countingCoins == 0)
				{
					coinCounter.Stop();
					EmitSignal(SignalName.CoinsCountDownEnd);
				}
			};
			coinCounter.Start();
		}
		else EmitSignal(SignalName.CoinsCountDownEnd);

		await ToSignal(this, SignalName.CoinsCountDownEnd);
		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

		GetNode<Label>("DiamondsLbl").Show();
		GetNode<Label>("DiamondsCount").Show();

		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

		GetNode<Label>("TimeLbl").Show();
		GetNode<Label>("TimeCount").Show();

		coinCounter = new Timer() { WaitTime = 0.05 };
		AddChild(coinCounter);
		coinCounter.Timeout += () => {
			_countingTime++;
			_timeCount.Text = _countingTime.ToString();
			if (_countingTime == RoundTime)
			{
				coinCounter.Stop();
				EmitSignal(SignalName.TimeCountUpEnd);
			}
		};
		coinCounter.Start();

		await ToSignal(this, SignalName.TimeCountUpEnd);
		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

		if (GameState.Bought.Contains(SkillItemName.BetterGrades)) GetNode<Sprite2D>("GradeA").Show();
		else GetNode<Sprite2D>("GradeF").Show();
		_animationPlayer.Play("grade");
	}

	private void _on_next_round()
	{

	}

	private void _on_store()
	{
		var storeScene = GD.Load<PackedScene>("res://Store/Store.tscn");
		var storeInstance = storeScene.Instantiate<Store>();
		GetTree().Root.AddChild(storeInstance);
		storeInstance.TreeExited += () => Show();
		Hide();
	}
}
