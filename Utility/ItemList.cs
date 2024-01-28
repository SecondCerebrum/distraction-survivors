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
				Description = "The essential tool for\nevery Discord admin",
				CoinsCost = 10
			},
			new StoreItemData {
				ID = SkillItemName.BlueItems,
				Title = "Blue Items",
				Texture = "res://Images/items/blue_items.jpg",
				Description = "You guys pay for that?",
				DiamondsCost = 1
			},
			new StoreItemData {
				ID = SkillItemName.CautionGrindy,
				Title = "Caution! Grindy",
				Texture = "res://Images/items/caution_grindy.jpg",
				Description = "You may loose your tires",
				CoinsCost = 20
			},
			new StoreItemData {
				ID = SkillItemName.GreedyCape,
				Title = "Greedy Cape",
				Texture = "res://Images/items/greedy_cape.jpg",
				Description = "Universal size for every\npublisher's CEO",
				DiamondsCost = 2
			},
			new StoreItemData {
				ID = SkillItemName.Placeholder,
				Title = "Random placeholder",
				Texture = "res://Images/items/placeholder.png",
				Description = "It holds a space so it's more\nspacious out there",
				CoinsCost = 30
			},
			new StoreItemData {
				ID = SkillItemName.Passat,
				Title = "A new (NFT) car",
				Texture = "res://Images/items/passat.jpg",
				Description = "Your neighbour will be jelous,\nalso you can't park there",
				DiamondsCost = 3
			},
			new StoreItemData {
				ID = SkillItemName.Extinguisher,
				Title = "Fire Extinguisher",
				Texture = "res://Images/items/extinguisher.jpg",
				Description = "Ignites emotions but extinguishes candles.",
				CoinsCost = 40
			},
		};
	}
}