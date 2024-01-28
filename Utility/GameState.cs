using System.Collections.Generic;

public static class GameState
{
    public static int Coins { get; set; } = 0;
    public static int Diamonds { get; set; } = 0;
    public static List<SkillItemName> Bought { get; set; } = new();
    public static List<string> AchievementsShown { get; set; } = new();
}

public enum SkillItemName
{
    // Skills
    CoinCollector,
    ActivePause,
    GloriousAchievements,
    LordSaveUs,
    CoinMagnet,
    BetterGrades,
    Stuffed,
    FrackingBlue,

    // Items
    Banhammer,
    BlueItems,
    CautionGrindy,
    GreedyCape,
    Passat,
    Placeholder
}