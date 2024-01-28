using Godot;
using System;

public partial class Store : Control
{
	private AnimationPlayer _animationPlayer;
	private AnimatedSprite2D _coin;
	private AnimatedSprite2D _diamond;
	private HBoxContainer _itemList;
	private HBoxContainer _skillList;
	private AudioStreamPlayer _openStoreSound;
	private AudioStreamPlayer _payment;
	private AudioStreamPlayer _yourLoss;
	private AudioStreamPlayer _terminalSound;
	private AudioStreamPlayer _goodCause;
	private Window _terminalWindow;
	private Label _coinsLbl;
	private Label _diamondsLbl;
	private bool _terminalUsed = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("dim_store");
		_coin = GetNode<AnimatedSprite2D>("HBoxContainer/CoinsLbl/Coin");
		_diamond = GetNode<AnimatedSprite2D>("HBoxContainer/DiamondsLbl/Diamond");
		_itemList = GetNode<HBoxContainer>("ScrollContainer/ItemList");
		_skillList = GetNode<HBoxContainer>("ScrollContainer2/SkillList");
		_openStoreSound = GetNode<AudioStreamPlayer>("OpenStoreSound");
		_payment = GetNode<AudioStreamPlayer>("Payment");
		_yourLoss = GetNode<AudioStreamPlayer>("YourLoss");
		_terminalSound = GetNode<AudioStreamPlayer>("TerminalSound");
		_goodCause = GetNode<AudioStreamPlayer>("GoodCause");
		_terminalWindow = GetNode<Window>("TerminalWindow");
		_coinsLbl = GetNode<Label>("HBoxContainer/CoinsLbl");
		_diamondsLbl = GetNode<Label>("HBoxContainer/DiamondsLbl");

		_coin.Play();
		_diamond.Play();
		_openStoreSound.Play();

		var storeItem = GD.Load<PackedScene>("res://Store/StoreItem.tscn");

		foreach (var item in ItemList.Get())
		{
			var newItem = storeItem.Instantiate<StoreItem>();
			newItem.Item = item;
			_itemList.AddChild(newItem);
		}

		foreach (var skill in SkillList.Get())
		{
			var newItem = storeItem.Instantiate<StoreItem>();
			newItem.Item = skill;
			_skillList.AddChild(newItem);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GameState.Bought.Contains(SkillItemName.ActivePause)) GetTree().Paused = true;
		_on_counters_update();
	}

	private void _on_back()
	{
		GetTree().Paused = false;
		GetTree().Root.RemoveChild(this);
	}

	private void _on_buy_more()
	{
		_terminalWindow.Show();
		_payment.Play();
		_terminalUsed = false;
	}

	private void _on_close_terminal()
	{
		_terminalWindow.Hide();
		_payment.Stop();
		if (!_terminalUsed) _yourLoss.Play();
		else _goodCause.Play();
	}

	private void _on_counters_update()
	{
		_coinsLbl.Text = "Coins: " + GameState.Coins.ToString();
		_diamondsLbl.Text = "Diamonds: " + GameState.Diamonds.ToString();
	}

	private void _on_terminal_window_input(InputEvent input)
	{
		if ((input is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Delete) || (input is InputEventScreenTouch touch && touch.Pressed))
		{
			_payment.Stop();
			_terminalSound.Play();
			GameState.Coins += 10;
			GameState.Diamonds += 1;
			_on_counters_update();
			_terminalUsed = true;
		}
	}
}
