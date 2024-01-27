using Godot;

public static class ItemList
{
	public static StoreItemData[] Get()
	{
		return new StoreItemData[] {
			new StoreItemData {
				ID = SkillItemName.Banhammer,
				Title = "BANhammer",
				Texture = "res://Images/items/banhammer.jpg",
				Description = "The essential tool for every Discord admin"
			},
			new StoreItemData {
				ID = SkillItemName.BlueItems,
				Title = "Blue Items",
				Texture = "res://Images/items/blue_items.jpg",
				Description = "You guys pay for that?"
			},
			new StoreItemData {
				ID = SkillItemName.CautionGrindy,
				Title = "Caution! Grindy",
				Texture = "res://Images/items/caution_grindy.jpg",
				Description = "You may loose your tires"
			},
			new StoreItemData {
				ID = SkillItemName.GreedyCape,
				Title = "Greedy Cape",
				Texture = "res://Images/items/greedy_cape.jpg",
				Description = "Universal size for every publisher's CEO"
			},
			new StoreItemData {
				ID = SkillItemName.Passat,
				Title = "Random placeholder",
				Texture = "res://Images/items/placeholder.png",
				Description = "It holds a space so it's more spacious out there"
			},
			new StoreItemData {
				ID = SkillItemName.Passat,
				Title = "A new (NFT) car",
				Texture = "res://Images/items/passat.jpg",
				Description = "Your neighbour will be jelous, also you can't park there"
			},
		};
	}
}