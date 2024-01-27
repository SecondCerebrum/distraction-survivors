using System.Collections.Generic;

public static class GameState
{
	public static int Coins { get; set; } = 0;
	public static int Diamonds { get; set; } = 0;
	public static List<SkillItemName> Bought { get; set; } = new List<SkillItemName>();
}

public enum SkillItemName
{
	// Skills
	CoinCollector,
	ActivePause,

	// Items
	Banhammer,
	BlueItems,
	CautionGrindy,
	GreedyCape,
	Passat,
	Placeholder,
}