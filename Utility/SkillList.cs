public static class SkillList
{
	public static StoreItemData[] Get()
	{
		return new StoreItemData[] {
			new StoreItemData {
				ID = SkillItemName.CoinCollector,
				Title = "Coin Collector",
				Texture = "res://Images/skills/coins.jpg",
				Description = "Oh, you thought you can collect coins from the beggining? Fool!"
			},
			new StoreItemData {
				ID = SkillItemName.ActivePause,
				Title = "Active Pause",
				Texture = "res://Images/skills/pause.jpg",
				Description = "It would be nice to pause the game while looking at the Store."
			},
		};
	}
}