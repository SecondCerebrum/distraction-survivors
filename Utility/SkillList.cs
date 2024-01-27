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
			new StoreItemData {
				ID = SkillItemName.GloriousAchievements,
				Title = "Glorious Achievements",
				Texture = "res://Images/items/cup.jpg",
				Description = "Achievement notifcations are shown for longer to be admire more."
			},
			new StoreItemData {
				ID = SkillItemName.LordSaveUs,
				Title = "Lord Save Us!",
				Texture = "res://Images/items/save.jpg",
				Description = "Your progress may be saved, but not loaded."
			},
			new StoreItemData {
				ID = SkillItemName.CoinMagnet,
				Title = "Plus Size",
				Texture = "res://Images/skills/fat_man_coins.jpg",
				Description = "You ate so much coins are pulled by your own gravity."
			},
		};
	}
}