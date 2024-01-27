using Godot;
using System;

public partial class store : Control
{
	private AnimationPlayer _animationPlayer;
	private AnimatedSprite2D _coin;
	private AnimatedSprite2D _diamond;
	private HBoxContainer _itemList;
	private HBoxContainer _skillList;
	private AudioStreamPlayer _openStoreSound;

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
		_openStoreSound.Play();

		var storeItem = GD.Load<PackedScene>("res://Store/store_item.tscn");

		StoreItemData[] items = {
			new StoreItemData {
				Title = "BANhammer",
				Texture = "res://Images/items/banhammer.jpg",
			},
			new StoreItemData {
				Title = "Blue Items",
				Texture = "res://Images/items/blue_items.jpg",
			},
			new StoreItemData {
				Title = "Caution! Grindy",
				Texture = "res://Images/items/caution_grindy.jpg",
			},
			new StoreItemData {
				Title = "Greedy Cape",
				Texture = "res://Images/items/greedy_cape.jpg",
			},
		};

		foreach (var item in items)
		{
			var newItem = storeItem.Instantiate();
			var button = newItem.GetNode<TextureButton>("TextureButton");
			var label = newItem.GetNode<Label>("TextureButton/Label");

			button.TextureNormal = ImageTexture.CreateFromImage(Image.LoadFromFile(item.Texture));
			label.Text = item.Title;

			_itemList.AddChild(newItem);
		}

		StoreItemData[] skills = {
			new StoreItemData {
				Title = "Coin Collector",
				Texture = "res://Images/skills/coins.jpg",
				Description = "Oh, you thought you can collect coins from the beggining? Fool!"
			},
			new StoreItemData {
				Title = "Active Pause",
				Texture = "res://Images/skills/pause.jpg",
				Description = "It would be nice to pause the game while looking at the Store."
			},
		};

		foreach (var skill in skills)
		{
			var newItem = storeItem.Instantiate();
			var button = newItem.GetNode<TextureButton>("TextureButton");
			var label = newItem.GetNode<Label>("TextureButton/Label");

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
}
