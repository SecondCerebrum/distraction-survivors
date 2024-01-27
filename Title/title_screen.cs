using Godot;

public partial class title_screen : Control
{
	private AnimatedSprite2D _coin1;
	private AnimatedSprite2D _coin2;
	private Window _readyWindow;
	private Window _exitWindow;
	private AudioStreamPlayer _giveUsMoney;
	private AudioStreamPlayer _whereAreYouGoing;
	private AudioStreamPlayer _btnHoverSound;
	private AudioStreamPlayer _noSavesToLoad;
	private ColorRect _popupMask;
	private Button _loadButton;
	private FileDialog _fileDialog;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_coin1 = GetNode<AnimatedSprite2D>("DLC/Coin1");
		_coin2 = GetNode<AnimatedSprite2D>("DLC/Coin2");
		_readyWindow = GetNode<Window>("ReadyWindow");
		_exitWindow = GetNode<Window>("ExitWindow");
		_giveUsMoney = GetNode<AudioStreamPlayer>("Play/GiveUsMoney");
		_whereAreYouGoing = GetNode<AudioStreamPlayer>("Exit/WhereAreYouGoing");
		_btnHoverSound = GetNode<AudioStreamPlayer>("BtnHoverSound");
		_noSavesToLoad = GetNode<AudioStreamPlayer>("Load/NoSavesToLoad");
		_popupMask = GetNode<ColorRect>("PopupMask");
		_loadButton = GetNode<Button>("Load");
		_fileDialog = GetNode<FileDialog>("FileDialog");

		if (GameState.Bought.Contains(SkillItemName.LordSaveUs))
		{
			_loadButton.Text = "Save";
			GetNode<Sprite2D>("Load/Sprite2D").Hide();
			GetNode<Sprite2D>("Load/Sprite2D2").Hide();
		}

		_coin1.Play();
		_coin2.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GameState.Bought.Contains(SkillItemName.LordSaveUs))
		{
			_loadButton.Text = "Save";
			GetNode<Sprite2D>("Load/Sprite2D").Hide();
			GetNode<Sprite2D>("Load/Sprite2D2").Hide();
		}
	}

	private void _on_btn_play_click_end()
	{
		var level = "res://World/World.tscn";
		var _level = GetTree().ChangeSceneToFile(level);
	}

	private void _on_store_enter()
	{
		var storeScene = GD.Load<PackedScene>("res://Store/Store.tscn");
		GetTree().Root.AddChild(storeScene.Instantiate());
		_readyWindow.Hide();
		_exitWindow.Hide();
		_popupMask.Hide();
	}

	private void _on_exit()
	{
		GetTree().Quit();
	}

	private void _on_play_pressed()
	{
		_readyWindow.Show();
		_giveUsMoney.Play();
		_popupMask.Show();
	}

	private void _on_exit_pressed()
	{
		_exitWindow.Show();
		_whereAreYouGoing.Play();
		_popupMask.Show();
	}

	private void _on_ready_popup_close()
	{
		_readyWindow.Hide();
		_popupMask.Hide();
	}

	private void _on_exit_popup_close()
	{
		_exitWindow.Hide();
		_popupMask.Hide();
	}

	private void _on_btn_hover()
	{
		_btnHoverSound.Play();
	}

	private void _on_load_btn()
	{
		if (GameState.Bought.Contains(SkillItemName.LordSaveUs))
		{
			_fileDialog.FileMode = FileDialog.FileModeEnum.SaveFile;
			_fileDialog.Filters = new string[] {"*.sav ; Save game"};
			_fileDialog.PopupCentered();
		}
		else _noSavesToLoad.Play();
	}

	private void _on_dialog_save(string path)
	{
		using (var file = FileAccess.Open(path, FileAccess.ModeFlags.Write))
		{
			file.StoreString(@"░░░░░░░░░▄░░░░░░░░░░░░░░▄░░░░
░░░░░░░░▌▒█░░░░░░░░░░░▄▀▒▌░░░
░░░░░░░░▌▒▒█░░░░░░░░▄▀▒▒▒▐░░░
░░░░░░░▐▄▀▒▒▀▀▀▀▄▄▄▀▒▒▒▒▒▐░░░
░░░░░▄▄▀▒░▒▒▒▒▒▒▒▒▒█▒▒▄█▒▐░░░
░░░▄▀▒▒▒░░░▒▒▒░░░▒▒▒▀██▀▒▌░░░
░░▐▒▒▒▄▄▒▒▒▒░░░▒▒▒▒▒▒▒▀▄▒▒▌░░
░░▌░░▌█▀▒▒▒▒▒▄▀█▄▒▒▒▒▒▒▒█▒▐░░
░▐░░░▒▒▒▒▒▒▒▒▌██▀▒▒░░░▒▒▒▀▄▌░
░▌░▒▄██▄▒▒▒▒▒▒▒▒▒░░░░░░▒▒▒▒▌░
▐▒▀▐▄█▄█▌▄░▀▒▒░░░░░░░░░░▒▒▒▐░
▐▒▒▐▀▐▀▒░▄▄▒▄▒▒▒▒▒▒░▒░▒░▒▒▒▒▌
▐▒▒▒▀▀▄▄▒▒▒▄▒▒▒▒▒▒▒▒░▒░▒░▒▒▐░
░▌▒▒▒▒▒▒▀▀▀▒▒▒▒▒▒░▒░▒░▒░▒▒▒▌░
░▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▒▄▒▒▐░░
░░▀▄▒▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▄▒▒▒▒▌░░
░░░░▀▄▒▒▒▒▒▒▒▒▒▒▄▄▄▀▒▒▒▒▄▀░░░
░░░░░░▀▄▄▄▄▄▄▀▀▀▒▒▒▒▒▄▄▀░░░░░
░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▀▀░░░░░░░░

You have been doged!");
		}
	}
}
