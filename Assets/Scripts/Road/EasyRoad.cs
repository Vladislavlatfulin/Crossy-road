public class EasyRoad : Road
{
    private void Start()
    {
        _timeBetweenSpawn = 1.5f;
        spawnChance = 6;
        _moveTime = 8;
    }
    
    protected override float _timeBetweenSpawn { get; set; }
    protected override int _moveTime { get; set; }
    protected override int spawnChance { get; set; }
}