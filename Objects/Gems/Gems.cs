using System;
using Godot;

public partial class Gems : Area2D
{
    private CollisionShape2D _collision;
    private int _speed = -1;

    private Sprite2D _sprite;

    public Node2D Target;
    public string Type = "red";

    public int Value = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("loot");
        _sprite = GetNode<Sprite2D>("Sprite");
        _collision = GetNode<CollisionShape2D>("CollisionShape");
        if (Value < 5)
        {
            _sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_red.png");
        }
        else if (Value < 15)
        {
            _sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_green.png");
            Type = "green";
        }
        else
        {
            _sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_blue.png");
            Type = "blue";
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (Target != null)
        {
            var random = new Random();
            var direction = 1;
            if (GameState.Bought.Contains(SkillItemName.CoinMagnet)) direction = -1;

            // if (ab == 1)
            // GlobalPosition = GlobalPosition.DirectionTo(Target.Position);
            // else
            GlobalPosition = GlobalPosition.MoveToward(Target.Position, _speed * direction);

            _speed += Convert.ToInt16(2 * delta);
        }
    }

    public void Collect()
    {
        _collision.CallDeferred("set", "disabled", true);
        _sprite.Visible = false;
    }

    public void OnSndCollectedFinished()
    {
        QueueFree();
    }
}