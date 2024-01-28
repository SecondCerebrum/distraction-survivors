using Godot;

public partial class SpawnInfo : Node
{
	public int TimeStart { get; set; }
	public int TimeEnd { get; set; }
	public PackedScene Enemy { get; set; }
	public int EnemyNum { get; set; }
	public int EnemySpawnDelay { get; set; }

	public int SpawnDelayCounter { get; set; }
	public int Level { get; set; }
}
