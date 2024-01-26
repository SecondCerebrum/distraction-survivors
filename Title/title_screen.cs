using Godot;
using System;

public partial class title_screen : Control
{
	private AnimatedSprite2D _coin1;
	private AnimatedSprite2D _coin2;
	private Window _readyWindow;
	private AudioStreamPlayer _audioStreamPlayer;
	private ColorRect _popupMask;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coin1 = GetNode<AnimatedSprite2D>("DLC/Coin1");
		_coin2 = GetNode<AnimatedSprite2D>("DLC/Coin2");
		_readyWindow = GetNode<Window>("ReadyWindow");
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("Play/GiveUsMoney");
		_popupMask = GetNode<ColorRect>("PopupMask");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_coin1.Play();
		_coin2.Play();
	}

	private void _on_btn_play_click_end()
	{
		var level = "res://World/World.tscn";
		var _level = GetTree().ChangeSceneToFile(level);
	}


	private void _on_exit_pressed()
	{
		GetTree().Quit();
	}

	private void _on_play_pressed()
	{
		_readyWindow.Show();
		_audioStreamPlayer.Play();
		_popupMask.Show();
	}
}