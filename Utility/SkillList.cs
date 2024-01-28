public static class SkillList
{
    public static StoreItemData[] Get()
    {
        return new StoreItemData[]
        {
            new()
            {
                ID = SkillItemName.CoinCollector,
                Title = "Coin Collector",
                Texture = "res://Images/skills/coins.jpg",
                Description = "Oh, you thought you can collect\ncoins from the beggining? Fool!",
                CoinsCost = 10
            },
            new()
            {
                ID = SkillItemName.ActivePause,
                Title = "Active Pause",
                Texture = "res://Images/skills/pause.jpg",
                Description = "It would be nice to pause the game\nwhile looking at the Store.",
                CoinsCost = 20
            },
            new()
            {
                ID = SkillItemName.GloriousAchievements,
                Title = "Moar Achievements",
                Texture = "res://Images/skills/lots_cups.jpg",
                Description = "Achievement notifcations are shown\nevery time, not only once.",
                CoinsCost = 50
            },
            new()
            {
                ID = SkillItemName.LordSaveUs,
                Title = "Lord Save Us!",
                Texture = "res://Images/items/save.jpg",
                Description = "Your progress may be saved,\nbut not loaded.",
                CoinsCost = 80
            },
            new()
            {
                ID = SkillItemName.CoinMagnet,
                Title = "Plus Size",
                Texture = "res://Images/skills/fat_man_coins.jpg",
                Description = "You ate so much, so coins are pulled\nby your own gravity.",
                CoinsCost = 100
            }
        };
    }
}