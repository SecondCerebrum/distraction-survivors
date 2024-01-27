public static class ItemList
{
	public static StoreItemData[] Get()
	{
		return new StoreItemData[] {
			new StoreItemData {
				ID = "banhammer",
				Title = "BANhammer",
				Texture = "res://Images/items/banhammer.jpg",
			},
			new StoreItemData {
				ID = "blueitems",
				Title = "Blue Items",
				Texture = "res://Images/items/blue_items.jpg",
			},
			new StoreItemData {
				ID = "cautiongrindy",
				Title = "Caution! Grindy",
				Texture = "res://Images/items/caution_grindy.jpg",
			},
			new StoreItemData {
				ID = "greedycape",
				Title = "Greedy Cape",
				Texture = "res://Images/items/greedy_cape.jpg",
			},
		};
	}
}