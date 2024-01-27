using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;


public partial class EnemySpawner : Node2D
{
	public List<SpawnInfo> Spawns = new List<SpawnInfo>();
	[Export] public int Time = 0;
	public CharacterBody2D Hero;
	private Timer _timer; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Hero = GetTree().GetFirstNodeInGroup("Hero") as CharacterBody2D;
		 _timer = GetNode<Timer>("Timer");
		_timer.Autostart = true;
		 _timer.Connect("timeout", new Callable(this, "onTimerTimeout"));
		 //Connect("ChangeTime", new Callable(Hero, "change_time"));
		PackedScene _level1 = GD.Load<PackedScene>("res://Enemy/Types/Level1/Level1.tscn");
		GD.Print("stj", _level1);
		SpawnInfo sth = new SpawnInfo
		{
			 Enemy = _level1,
			SpawnDelayCounter = 0,
			EnemyNum = 1,
			TimeStart = 0,
			TimeEnd = 30,
			EnemySpawnDelay = 0
		};
		GD.Print("stj", sth);
		Spawns.Add(sth);
	}
	
	public void onTimerTimeout()
	{
		GD.Print("onTimerTimeout");
		Time++;
		var EnemySprawns = Spawns;
		foreach (SpawnInfo Es in EnemySprawns)
		{
			if (Time > Es.TimeStart && Time < Es.TimeEnd)
			{
				if (Es.SpawnDelayCounter < Es.EnemySpawnDelay)
				{
					Es.SpawnDelayCounter++;
				}
				else
				{
					Es.SpawnDelayCounter = 0;
					var Counter = 0;
					while (Counter < Es.EnemyNum)
					{
						var EnemySpawn = Es.Enemy.Instantiate();
						AddChild(EnemySpawn);
						GD.Print(EnemySpawn);
						Counter++;
					}
				}
			}
		}
		//EmitSignal("ChangeTime", Time);
	}
	
	private Vector2 GetRandomPosition()
	{
		var Vpr = GetViewportRect().Size;
		var TopLeft = new Vector2(Hero.GlobalPosition.X - Vpr.X / 2, Hero.GlobalPosition.Y - Vpr.Y / 2);
		var TopRight = new Vector2(Hero.GlobalPosition.X + Vpr.X / 2, Hero.GlobalPosition.Y - Vpr.Y / 2);
		var BottomLeft = new Vector2(Hero.GlobalPosition.X - Vpr.X / 2, Hero.GlobalPosition.Y + Vpr.Y / 2);
		var BottomRight = new Vector2(Hero.GlobalPosition.X + Vpr.X / 2, Hero.GlobalPosition.Y + Vpr.Y / 2);
		string[] Positions = { "up", "down", "left", "right" };
		
		Random random = new Random();
		int r = random.Next(Positions.Length);
		string RandomPosition = Positions[r];

		var SpawnPosition1 = Vector2.Zero;
		var SpawnPosition2 = Vector2.Zero;

		switch (RandomPosition)
		{
			case "up":
				SpawnPosition1 = TopLeft;
				SpawnPosition2 = TopRight;
				break;
			case "down":
				SpawnPosition1 = BottomLeft;
				SpawnPosition2 = BottomRight;
				break;
			case "left":
				SpawnPosition1 = TopLeft;
				SpawnPosition2 = BottomLeft;
				break;
			case "right":
				SpawnPosition1 = TopRight;
				SpawnPosition2 = BottomRight;
				break;
		}

		return new Vector2(SpawnPosition1.X, SpawnPosition1.Y);
	}
}
