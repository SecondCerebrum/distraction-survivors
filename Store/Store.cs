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
		_terminalWindow = GetNode<Window>("TerminalWindow");
		_coinsLbl = GetNode<Label>("HBoxContainer/CoinsLbl");
		_diamondsLbl = GetNode<Label>("HBoxContainer/DiamondsLbl");

		_openStoreSound.Play();
		_on_counters_update();

		var storeItem = GD.Load<PackedScene>("res://Store/StoreItem.tscn");

		foreach (var item in ItemList.Get())
		{
			var newItem = storeItem.Instantiate<StoreItem>();
			newItem.ID = item.ID;
			newItem.Bought = GameState.Bought.Contains(item.ID);
			var button = newItem.GetNode<TextureButton>("VBoxContainer/TextureButton");
			var label = newItem.GetNode<Label>("VBoxContainer/Label");

			button.TextureNormal = ImageTexture.CreateFromImage(Image.LoadFromFile(item.Texture));
			label.Text = item.Title;

			_itemList.AddChild(newItem);
		}

		foreach (var skill in SkillList.Get())
		{
			var newItem = storeItem.Instantiate<StoreItem>();
			newItem.ID = skill.ID;
			newItem.Bought = GameState.Bought.Contains(skill.ID);
			var button = newItem.GetNode<TextureButton>("VBoxContainer/TextureButton");
			var label = newItem.GetNode<Label>("VBoxContainer/Label");

			button.TextureNormal = ImageTexture.CreateFromImage(Image.LoadFromFile(skill.Texture));
			button.TooltipText = skill.Description;
			label.Text = skill.Title;

			_skillList.AddChild(newItem);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_coin.Play();
		_diamond.Play();
	}

	private void _on_back()
	{
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
	}

	private void _on_counters_update()
	{
		_coinsLbl.Text = "Coins: " + GameState.Coins.ToString("000");
		_diamondsLbl.Text = "Diamonds: " + GameState.Diamonds.ToString("000");
	}

	private void _on_terminal_window_input(InputEvent input)
	{
		if ((input is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Delete) || (input is InputEventScreenTouch touch && touch.Pressed))
		{
			_payment.Stop();
			_terminalSound.Play();
			GameState.Coins += 20;
			GameState.Diamonds += 5;
			_on_counters_update();
			_terminalUsed = true;
		}
	}
}
