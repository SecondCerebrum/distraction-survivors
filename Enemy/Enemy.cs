using Godot;

public partial class Enemy : CharacterBody2D
{
    private readonly float _kockbackRecovery = 3.5f;
    private int _demage = 1;

    private int _experience = 1;

    private CharacterBody2D _hero;

    private int _hp = 10;

    private Vector2 _kockback = Vector2.Zero;

    private float _movementSpeed = 120.0f;
    private Sprite2D _sprite;

    public void OnHurtBoxHurt(int damage, int angle, int kockbackAmount)
    {
        _hp -= damage;
        // _kockback = angle * kockbackAmount;
        if (_hp <= 0) death();
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _hero = GetTree().Root.GetNode<CharacterBody2D>("World/Hero");
        _sprite = GetNode<Sprite2D>("Sprite");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        _kockback = _kockback.MoveToward(Vector2.Zero, _kockbackRecovery);
        var direction = Position.DirectionTo(_hero.GlobalPosition);
        Velocity = direction * _movementSpeed;
        // Velocity += _kockback;
        MoveAndSlide();

        if (direction.X > 0.1) _sprite.FlipH = true;
        else if (direction.X < -0.1) _sprite.FlipH = false;
    }

    public void death()
    {
        EmitSignal("RemoveFromArray", this);
        // animation of death
        // add loot
        QueueFree();
    }
}