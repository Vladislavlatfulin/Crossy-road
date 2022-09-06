public class HardRoad : Road
{
    private void Start()
    {
        _timeBetweenSpawn = 0.5f;
        spawnChance = 2;
        _moveTime = 3;
    }
    
    protected override float _timeBetweenSpawn { get; set; }
    protected override int _moveTime { get; set; }
    protected override int spawnChance { get; set; }
}