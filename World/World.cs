using System;
using Godot;

public partial class World : Node2D
{
	private Achievement _achievement;
	private CollectCoinsWindow _collectCoinsWindow;
	private EnemySpawner _enemySpawner;
	private Hero _hero;
	private Random _random;
	private Vector2 _viewportSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_collectCoinsWindow = GetNode<CollectCoinsWindow>("CollectCoinsWindow");
		_viewportSize = GetViewportRect().Size;
		_random = new Random();
		_achievement = GetNode<Achievement>("Achievement");
		_hero = GetNode<Hero>("Hero");
		_enemySpawner = GetNode<EnemySpawner>("EnemySpawner");
	}

	public void RestartGame()
	{
		_hero.Run();
		_enemySpawner.Run();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
		var skillSelect = GD.Load<PackedScene>("res://World/Components/SkillSelect.tscn");
		var skillSelectInstance = skillSelect.Instantiate<SkillSelect>();
		AddChild(skillSelectInstance);
		skillSelectInstance.Show();
	}

	private void _on_round_summary()
	{
		OpenRoundSummary(20, 5, 60);
	}

	public void OpenRoundSummary(int roundCoins, int roundDiamonds, int roundTime)
	{
		GD.Print("roundCoins ", roundCoins, " roundDiamonds", roundDiamonds, " roundTime", roundTime);
		var summary = GD.Load<PackedScene>("res://World/Components/RoundSummary.tscn");
		var summaryInstance = summary.Instantiate<RoundSummary>();
		AddChild(summaryInstance);
		summaryInstance.Run(roundCoins, roundDiamonds, roundTime);
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
