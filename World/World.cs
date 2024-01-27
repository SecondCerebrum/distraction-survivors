using Godot;
using System;

public partial class World : Node2D
{
	private SkillSelect _skillSelect;
	private CollectCoinsWindow _collectCoinsWindow;
	private Vector2 _viewportSize;
	private Random _random;
	private Label _coinsLbl;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillSelect = GetNode<SkillSelect>("SkillSelectWindow");
		_collectCoinsWindow = GetNode<CollectCoinsWindow>("CollectCoinsWindow");
		_viewportSize = GetViewportRect().Size;
		_random = new Random();
		_coinsLbl = GetNode<Label>("CoinsLbl");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_coinsLbl.Text = "Coins: " + GameState.Coins.ToString("000");
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

	private void _on_collect_popup()
	{
		Vector2I[] positions = {
			new Vector2I(50, 70),
			new Vector2I((int)_viewportSize.X - _collectCoinsWindow.Size.X - 50, 70),
			new Vector2I(50, (int)_viewportSize.Y - _collectCoinsWindow.Size.Y - 50),
			new Vector2I((int)_viewportSize.X - _collectCoinsWindow.Size.X - 50, (int)_viewportSize.Y - _collectCoinsWindow.Size.Y - 50),
		};
		_collectCoinsWindow.Position = positions[_random.Next(0, positions.Length)];
		_collectCoinsWindow.Show();
	}
}
