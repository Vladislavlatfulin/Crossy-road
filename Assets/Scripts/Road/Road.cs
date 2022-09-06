using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Road : MonoBehaviour
{
    [SerializeField] protected List<Transform> _spawnPosition;
    [Inject] private ICarPool _carPool;
    protected abstract int spawnChance { get; set; }
    protected abstract float _timeBetweenSpawn { get; set; }
    protected abstract int _moveTime { get; set; }
    private float _timeSpawn { get; set; }
    
    public void Update()
    {
        if (_timeSpawn <= 0)
        {
            SpawnCar();

            _timeSpawn = _timeBetweenSpawn;
        }
        else
        {
            _timeSpawn -= Time.deltaTime;
        }
    }

    private void SpawnCar()
    {
        var randomChance = Random.Range(0, 10);
        if (randomChance >= spawnChance)
        {
            Random.Range(0, _spawnPosition.Count);
            var randomSpawner = _spawnPosition[Random.Range(0, _spawnPosition.Count)];
            var car = _carPool.Get();
            car.gameObject.SetActive(true);
            car.gameObject.transform.position = randomSpawner.position;
            car.Move(_moveTime);
        }
    }
}