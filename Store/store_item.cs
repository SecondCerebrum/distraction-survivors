using Godot;
using System;

public partial class store_item : MarginContainer
{
	[Export] public bool Bought { get; set; }

	private ColorRect _dim;
	private Button _buy;
	private Button _bought;
	private AudioStreamPlayer _buySound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_dim = GetNode<ColorRect>("TextureButton/Dim");
		_buy = GetNode<Button>("TextureButton/Buy");
		_bought = GetNode<Button>("TextureButton/Bought");
		_buySound = GetNode<AudioStreamPlayer>("BuySound");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_buy()
	{
		_buySound.Play();
		Bought = true;
		_dim.Show();
		_buy.Hide();
		_bought.Show();
	}

	private void _on_hover()
	{
		if (Bought) _bought.Show();
		else _buy.Show();
	}

	private void _on_hover_end()
	{
		_bought.Hide();
		_buy.Hide();
	}
}
