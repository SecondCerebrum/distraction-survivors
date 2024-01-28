using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class EnemySpawner : Node2D
{
    [Signal]
    public delegate void ChangeTimeEventHandler(int time);

    [Signal]
    public delegate void GameOverEventHandler();

    private CharacterBody2D _hero;

    private List<SpawnInfo> _spawns = new();
    private int _time;
    private int _timeLimit = 30;
    private Timer _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _hero = GetTree().Root.GetNode<CharacterBody2D>("World/Hero");
        _timer = GetNode<Timer>("Timer");
        _timer.Connect("timeout", new Callable(this, "onTimerTimeout"));
        var levels = new List<int> { 1, 2, 3, 4, 5 };
        var packages = levels.Select(level =>
            {
                var path = $"res://Enemy/Types/Level{level}/Level{level}.tscn";
                return GD.Load<PackedScene>(path);
            })
            .ToList();
        _spawns = packages.Select((package, index) =>
        {
            var timestart = index > 1 ? 8 * index : 0;
            var timeend = index > 1 ? 8 * index + 15 : 15;
            var spawnDelay = 0;
            return new SpawnInfo
            {
                Enemy = package,
                Level = index + 1,
                SpawnDelayCounter = 0,
                EnemyNum = 1,
                TimeStart = timestart,
                TimeEnd = timeend,
                EnemySpawnDelay = spawnDelay
            };
        }).ToList();
        Run();
    }

    public void Run()
    {
        _time = 0;
        _timer.Start();
    }

    public void onTimerTimeout()
    {
        _time++;
        var hasThisIsSparta = GameState.Bought.Contains(SkillItemName.ThisIsSparta);
        if (_time > _timeLimit && !hasThisIsSparta)
        {
            EmitSignal(SignalName.GameOver);
            _timer.Stop();
            return;
        }

        var EnemySprawns = _spawns;
        foreach (var Es in EnemySprawns)
            if (_time > Es.TimeStart && _time < Es.TimeEnd)
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
                        var EnemySpawn = Es.Enemy.Instantiate() as Enemy;
                        EnemySpawn.Position = GetRandomPosition();
                        var size = new Random().Next(1, Es.Level);
                        EnemySpawn.Demage *= size;
                        EnemySpawn.Hp *= size;
                        EnemySpawn.ApplyScale(new Vector2(size, size));
                        AddChild(EnemySpawn);
                        Counter++;
                    }
                }
            }

        EmitSignal(SignalName.ChangeTime, _time);
    }

    private Vector2 GetRandomPosition()
    {
        var vpr = GetViewportRect().Size;
        var topLeft = new Vector2(_hero.Position.X - vpr.X / 2, _hero.Position.Y - vpr.Y / 2);
        var topRight = new Vector2(_hero.Position.X + vpr.X / 2, _hero.Position.Y - vpr.Y / 2);
        var bottomLeft = new Vector2(_hero.Position.X - vpr.X / 2, _hero.Position.Y + vpr.Y / 2);
        var bottomRight = new Vector2(_hero.Position.X + vpr.X / 2, _hero.Position.Y + vpr.Y / 2);
        string[] positions = { "up", "down", "left", "right" };

        var random = new Random();
        var r = random.Next(positions.Length);
        var randomPosition = positions[r];

        var spawnPosition1 = Vector2.Zero;
        var spawnPosition2 = Vector2.Zero;

        switch (randomPosition)
        {
            case "up":
                spawnPosition1 = topLeft;
                spawnPosition2 = topRight;
                break;
            case "down":
                spawnPosition1 = bottomLeft;
                spawnPosition2 = bottomRight;
                break;
            case "left":
                spawnPosition1 = topLeft;
                spawnPosition2 = bottomLeft;
                break;
            case "right":
                spawnPosition1 = topRight;
                spawnPosition2 = bottomRight;
                break;
        }

        var randomX = random.Next(Convert.ToInt16(spawnPosition1.X), Convert.ToInt16(spawnPosition2.X));
        var randomY = random.Next(Convert.ToInt16(spawnPosition1.Y), Convert.ToInt16(spawnPosition2.Y));
        return new Vector2(randomX, randomY);
    }
}