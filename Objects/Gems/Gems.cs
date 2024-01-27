using System;
using Godot;

public partial class Gems : Area2D
{
    private CollisionShape2D _collision;
    private int _speed = -1;

    private Sprite2D _sprite;

    private Node2D _target;

    public int value = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("loot");
        _sprite = GetNode<Sprite2D>("Sprite");
        _collision = GetNode<CollisionShape2D>("CollisionShape");
        if (value < 5)
            _sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_red.png");
        else if (value < 15)
            _sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_green.png");
        else
            _sprite.Texture = GD.Load<Texture2D>("res://Objects/Gems/Images/Gem_blue.png");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (_target != null)
        {
            GlobalPosition = GlobalPosition.MoveToward(_target.GlobalPosition, _speed);
            _speed += Convert.ToInt16(2 * delta);
        }
    }

    public int collect()
    {
        _collision.CallDeferred("set", "disabled", true);
        _sprite.Visible = false;
        return value;
    }

    public void OnSndCollectedFinished()
    {
        QueueFree();
    }
}