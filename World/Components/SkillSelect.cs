using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class SkillSelect : Window
{
	private Random _random;
	private HBoxContainer _skillList;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillList = GetNode<HBoxContainer>("ItemList");
		_random = new Random();

		var storeItem = GD.Load<PackedScene>("res://Store/StoreItem.tscn");

		var itemsToShow = new List<StoreItemData>();

		var availableSkills = SkillList.Get().Where(skill => !GameState.Bought.Contains(skill.ID)).ToArray();
		var availableItems = ItemList.Get().Where(item => !GameState.Bought.Contains(item.ID)).ToArray();

		if (availableSkills.Length > 0) itemsToShow.Add(availableSkills[_random.Next(availableSkills.Length)]);
		if (availableItems.Length > 0)
		{
			itemsToShow.Add(availableItems[_random.Next(availableItems.Length)]);
			itemsToShow.Add(availableItems[_random.Next(availableItems.Length)]);
		}

		foreach (var item in itemsToShow)
		{
			var newItem = storeItem.Instantiate<StoreItem>();
			newItem.Item = item;
			// newItem.FreeCost = true;
			newItem.Select += _on_close_requested;
			_skillList.AddChild(newItem);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GameState.Bought.Contains(SkillItemName.ActivePause) && Visible) GetTree().Paused = true;
	}

	private void _on_close_requested()
	{
		GetTree().Paused = false;
		GetParent().RemoveChild(this);
	}
}
