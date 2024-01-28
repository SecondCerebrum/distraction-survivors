using Godot;
using System;
using System.Drawing;

public partial class StoreItem : MarginContainer
{
	[Export] public StoreItemData Item { get; set; }
	[Export] public bool Bought { get; set; }
	[Export] public bool FreeCost { get; set; } = false;
	[Signal] public delegate void SelectEventHandler();

	private ColorRect _dim;
	private Button _buy;
	private Button _bought;
	private TextureButton _textureBtn;
	private Label _title;
	private Label _cost;
	private AudioStreamPlayer _buySound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_dim = GetNode<ColorRect>("VBoxContainer/TextureButton/Dim");
		_buy = GetNode<Button>("VBoxContainer/TextureButton/Buy");
		_bought = GetNode<Button>("VBoxContainer/TextureButton/Bought");
		_buySound = GetNode<AudioStreamPlayer>("BuySound");
		_textureBtn = GetNode<TextureButton>("VBoxContainer/TextureButton");
		_title = GetNode<Label>("VBoxContainer/Label");
		_cost = GetNode<Label>("VBoxContainer/TextureButton/Cost");

		Bought = GameState.Bought.Contains(Item.ID);
		if (Bought) _dim.Show();

		_textureBtn.TextureNormal = ImageTexture.CreateFromImage(Image.LoadFromFile(Item.Texture));
		_textureBtn.TooltipText = Item.Description;
		_title.Text = Item.Title;
		if (Item.DiamondsCost > 0) _cost.Text = Item.DiamondsCost.ToString();
		else _cost.Text = Item.CoinsCost.ToString();
		if (FreeCost) _buy.Text = "Choose";

		var coin = GetNode<AnimatedSprite2D>("VBoxContainer/TextureButton/Cost/Coin");
		var diamond = GetNode<AnimatedSprite2D>("VBoxContainer/TextureButton/Cost/Diamond");
		coin.Play();
		diamond.Play();
		if (Item.DiamondsCost > 0) diamond.Show();
		else coin.Show();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!can_buy()) _cost.AddThemeColorOverride("font_color", Colors.Red);
		else _cost.RemoveThemeColorOverride("font_color");
	}

	private void _on_buy()
	{
		_buySound.Play();
		Bought = true;
		if (FreeCost) {
			GameState.Bought.Add(Item.ID);
			EmitSignal(SignalName.Select);
		}
		else if (can_buy())
		{
			if (Item.DiamondsCost > 0) GameState.Diamonds -= Item.DiamondsCost;
			else GameState.Coins -= Item.CoinsCost;
			GameState.Bought.Add(Item.ID);
		}
		_dim.Show();
		_buy.Hide();
		_cost.Hide();
		_bought.Show();
	}

	private void _on_hover()
	{
		if (Bought) _bought.Show();
		else if (FreeCost || can_buy()) _buy.Show();
	}

	private void _on_hover_end()
	{
		_bought.Hide();
		_cost.Hide();
		_buy.Hide();
	}

	private bool can_buy()
	{
		if (Item.DiamondsCost > 0) return GameState.Diamonds >= Item.DiamondsCost;
		else return GameState.Coins >= Item.CoinsCost;
	}
}
