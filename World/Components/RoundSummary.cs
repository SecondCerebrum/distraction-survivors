using Godot;

public partial class RoundSummary : Window
{
	[Signal]
	public delegate void CoinsCountDownEndEventHandler();

	[Signal]
	public delegate void CoinsCountUpEndEventHandler();

	[Signal]
	public delegate void DiamondsCountUpEndEventHandler();

	[Signal]
	public delegate void TimeCountUpEndEventHandler();

	private AnimationPlayer _animationPlayer;
	private Label _coinsCount;
	private Label _coinsLbl;
	private AudioStreamPlayer _countingSound;
	private Label _diamondsCount;
	private Button _nextRound;
	private AudioStreamPlayer _punchSound;
	private Button _store;
	private Label _timeCount;
	[Export] public int RoundCoins { get; set; } = 20;
	[Export] public int RoundDiamonds { get; set; } = 2;
	[Export] public int RoundTime { get; set; } = 40;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		_nextRound = GetNode<Button>("NextRound");
		_store = GetNode<Button>("Store");
		_coinsLbl = GetNode<Label>("CoinsLbl");
		_coinsCount = GetNode<Label>("CoinsCount");
		_diamondsCount = GetNode<Label>("DiamondsCount");
		_timeCount = GetNode<Label>("TimeCount");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_countingSound = GetNode<AudioStreamPlayer>("CountingSound");
		_punchSound = GetNode<AudioStreamPlayer>("PunchSound");
		GetNode<AnimatedSprite2D>("CoinsLbl/Coin").Play();
		GetNode<AnimatedSprite2D>("DiamondsLbl/Diamond").Play();
		GetNode<AnimatedSprite2D>("Store/Coin1").Play();
		GetNode<AnimatedSprite2D>("Store/Coin2").Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void Run(int roundCoins, int roundDiamonds, int roundTime)
	{
		RoundCoins = roundCoins;
		RoundDiamonds = roundDiamonds;
		RoundTime = roundTime;

		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		_coinsLbl.Show();
		_coinsCount.Show();

		var counter = new Timer { WaitTime = 0.1 };
		AddChild(counter);
		var countingCoins = 0;
		counter.Timeout += () =>
		{
			_coinsCount.Text = countingCoins.ToString();
			if (countingCoins == RoundCoins)
			{
				counter.Stop();
				_countingSound.Stop();
				EmitSignal(SignalName.CoinsCountUpEnd);
			}

			countingCoins++;
		};
		counter.Start();
		_countingSound.Play();
		await ToSignal(this, SignalName.CoinsCountUpEnd);
		GD.Print("count end");

		if (!GameState.Bought.Contains(SkillItemName.CoinCollector))
		{
			GetNode<Label>("NoSkill").Show();
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

			counter = new Timer { WaitTime = 0.05 };
			AddChild(counter);
			counter.Timeout += () =>
			{
				_coinsCount.Text = countingCoins.ToString();
				if (countingCoins == 0)
				{
					counter.Stop();
					_countingSound.Stop();
					EmitSignal(SignalName.CoinsCountDownEnd);
				}

				countingCoins--;
			};
			counter.Start();
			_countingSound.Play();
		}
		else
		{
			EmitSignal(SignalName.CoinsCountDownEnd);
		}

		await ToSignal(this, SignalName.CoinsCountDownEnd);
		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

		GetNode<Label>("DiamondsLbl").Show();
		GetNode<Label>("DiamondsCount").Show();

		counter = new Timer { WaitTime = 0.1 };
		AddChild(counter);
		var countingDiamonds = 0;
		counter.Timeout += () =>
		{
			_diamondsCount.Text = countingDiamonds.ToString();
			if (countingDiamonds == RoundDiamonds)
			{
				counter.Stop();
				_countingSound.Stop();
				EmitSignal(SignalName.DiamondsCountUpEnd);
			}

			countingDiamonds++;
		};
		counter.Start();
		_countingSound.Play();
		await ToSignal(this, SignalName.DiamondsCountUpEnd);

		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

		GetNode<Label>("TimeLbl").Show();
		GetNode<Label>("TimeCount").Show();

		counter = new Timer { WaitTime = 0.05 };
		AddChild(counter);
		var countingTime = 0;
		counter.Timeout += () =>
		{
			_timeCount.Text = countingTime.ToString();
			if (countingTime == RoundTime)
			{
				counter.Stop();
				_countingSound.Stop();
				EmitSignal(SignalName.TimeCountUpEnd);
			}

			countingTime++;
		};
		counter.Start();
		_countingSound.Play();

		await ToSignal(this, SignalName.TimeCountUpEnd);
		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

		if (GameState.Bought.Contains(SkillItemName.BetterGrades)) GetNode<Sprite2D>("GradeA").Show();
		else GetNode<Sprite2D>("GradeF").Show();
		_animationPlayer.Play("grade");
		_punchSound.Play();
	}

	private void _on_next_round()
	{
		// GetTree().ReloadCurrentScene();
		// GetParent().RemoveChild(this);
		var level = "res://World/World.tscn";
		var _level = GetTree().ChangeSceneToFile(level);
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
