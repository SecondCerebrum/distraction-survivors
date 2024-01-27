public class StoreItemData
{
	public SkillItemName ID { get; set; }
	public string Title { get; set; }
	public string Texture { get; set; }
	public string Description { get; set; }
	public int CoinsCost { get; set; } = 0;
	public int DiamondsCost { get; set; } = 0;
	public bool Bought { get; set; } = false;
}