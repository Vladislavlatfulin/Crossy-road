public class MediumRoad : Road
{
    private void Start()
    {
        _timeBetweenSpawn = 1f;
        spawnChance = 3;
        _moveTime = 5;
    }
    
    protected override float _timeBetweenSpawn { get; set; }
    protected override int _moveTime { get; set; }
    protected override int spawnChance { get; set; }
}