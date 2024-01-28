using System;
using Godot;

public partial class World : Node2D
{
	private Achievement _achievement;
	private Label _coinsLbl;
	private CollectCoinsWindow _collectCoinsWindow;
	private Hero _hero;
	private Random _random;
	private SkillSelect _skillSelect;
	private RoundSummary _roundSummary;
	private Vector2 _viewportSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_skillSelect = GetNode<SkillSelect>("SkillSelectWindow");
		_roundSummary = GetNode<RoundSummary>("RoundSummary");
		_collectCoinsWindow = GetNode<CollectCoinsWindow>("CollectCoinsWindow");
		_viewportSize = GetViewportRect().Size;
		_random = new Random();
		_coinsLbl = GetNode<Label>("CoinsLbl");
		_achievement = GetNode<Achievement>("Achievement");
		_hero = GetNode<Hero>("Hero");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_coinsLbl.Text = "Coins: " + GameState.Coins.ToString("000");

		if (_hero.Position.X < -20 || _hero.Position.X > GetViewportRect().Size.X + 20 ||
				_hero.Position.Y < -20 || _hero.Position.Y > GetViewportRect().Size.Y + 20)
		{
			GetNode<Achievement>("Achievement").Run("bounds", "There are no bounds if you ask");
			GameState.AchievementsShown.Add("bounds");
		}
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

	private void _on_round_summary()
	{
		_roundSummary.Show();
		_roundSummary.Run();
	}

	private void _on_collect_popup()
	{
		Vector2I[] positions =
		{
			new(50, 70),
			new((int)_viewportSize.X - _collectCoinsWindow.Size.X - 50, 70),
			new(50, (int)_viewportSize.Y - _collectCoinsWindow.Size.Y - 50),
			new((int)_viewportSize.X - _collectCoinsWindow.Size.X - 50,
				(int)_viewportSize.Y - _collectCoinsWindow.Size.Y - 50)
		};
		_collectCoinsWindow.Position = positions[_random.Next(0, positions.Length)];
		_collectCoinsWindow.Show();
	}
}
