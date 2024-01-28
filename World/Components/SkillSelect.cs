using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SkillSelect : Window
{
	private HBoxContainer _skillList;
	private Random _random;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillList = GetNode<HBoxContainer>("ItemList");
		_random = new Random();

		var storeItem = GD.Load<PackedScene>("res://Store/StoreItem.tscn");

		var itemsToShow = new List<StoreItemData>();

		var availableSkills = SkillList.Get().Where(skill => !GameState.Bought.Contains(skill.ID)).ToArray();
		var availableItems = ItemList.Get().Where(item => !GameState.Bought.Contains(item.ID)).ToArray();

		if (availableSkills.Length > 0)
		{
			itemsToShow.Add(availableSkills[_random.Next(availableSkills.Length)]);
		}

		foreach (var items in itemsToShow)
		{
			var newItem = storeItem.Instantiate<StoreItem>();
			newItem.ID = items.ID;
			newItem.Bought = GameState.Bought.Contains(items.ID);
			var button = newItem.GetNode<TextureButton>("VBoxContainer/TextureButton");
			var label = newItem.GetNode<Label>("VBoxContainer/Label");

			button.TextureNormal = ImageTexture.CreateFromImage(Image.LoadFromFile(items.Texture));
			button.TooltipText = items.Description;
			label.Text = items.Title;

			_skillList.AddChild(newItem);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void _on_close_requested()
	{
		Hide();
	}
}
