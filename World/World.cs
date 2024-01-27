using Godot;
using System;

public partial class World : Node2D
{
	private SkillSelect _skillSelect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillSelect = GetNode<SkillSelect>("SkillSelectWindow");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_back_to_menu()
	{
		var level = "res://Title/title_screen.tscn";
		GetTree().ChangeSceneToFile(level);
	}

	private void _on_store_enter()
	{
		var storeScene = GD.Load<PackedScene>("res://Store/Store.tscn");
		var storeInstance = storeScene.Instantiate<Store>();
		var storeBg = storeInstance.GetNode<TextureRect>("TextureRect");
		storeBg.SelfModulate = Color.FromHtml("#ffffff80");
		GetTree().Root.AddChild(storeInstance);
		if (GameState.Bought.Contains(SkillItemName.ActivePause)) GetTree().Paused = true;
	}

	private void _on_skill_select()
	{
		_skillSelect.Show();
	}
}
