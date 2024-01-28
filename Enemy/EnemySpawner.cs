using System;
using System.Collections.Generic;
using Godot;

public partial class EnemySpawner : Node2D
{
    private readonly List<SpawnInfo> _spawns = new();
    private CharacterBody2D _hero;
    private int _time;
    private Timer _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _hero = GetTree().Root.GetNode<CharacterBody2D>("World/Hero");
        _timer = GetNode<Timer>("Timer");
        _timer.Autostart = true;
        _timer.Connect("timeout", new Callable(this, "onTimerTimeout"));
        // Connect("ChangeTime", new Callable(_hero, "change_time"));
        var _level1 = GD.Load<PackedScene>("res://Enemy/Types/Level1/Level1.tscn");
        var sth = new SpawnInfo
        {
            Enemy = _level1,
            SpawnDelayCounter = 0,
            EnemyNum = 1,
            TimeStart = 0,
            TimeEnd = 10,
            EnemySpawnDelay = 0
        };
        _spawns.Add(sth);
    }

    public void onTimerTimeout()
    {
        GD.Print("onTimerTimeout");
        _time++;
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
                        var EnemySpawn = Es.Enemy.Instantiate() as CharacterBody2D;
                        EnemySpawn.Position = GetRandomPosition();
                        AddChild(EnemySpawn);
                        Counter++;
                    }
                }
            }
        //EmitSignal("ChangeTime", Time);
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

        GD.Print(randomPosition);
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