using Godot;
using System;

public partial class Achievement : Control
{

	[Export] public string AchievementTitle { get; set; }
	private Label _title;
	private AnimationPlayer _animationPlayer;
	private AudioStreamPlayer _audioStreamPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_title = GetNode<Label>("TextureRect/Title");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Run(string title)
	{
		if (title is not null) _title.Text = title;
		else _title.Text = AchievementTitle ?? "You found this placeholder";
		_animationPlayer.Play("achievement");
		_audioStreamPlayer.Play();
	}
}
