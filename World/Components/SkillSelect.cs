using Godot;
using System;

public partial class SkillSelect : Window
{
	private HBoxContainer _skillList;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillList = GetNode<HBoxContainer>("ItemList");

		var storeItem = GD.Load<PackedScene>("res://Store/StoreItem.tscn");

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

	}
}
