using System.Collections.Generic;

public static class GameState
{
	public static int Coins { get; set; } = 20;
	public static int Diamonds { get; set; } = 0;
	public static List<string> Bought { get; set; } = new List<string>();
}