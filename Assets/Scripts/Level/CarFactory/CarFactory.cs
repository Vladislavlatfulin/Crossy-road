using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CarFactory : IFactory
{
    private DiContainer _container;
    private List<GameObject> _carPrefabs;

    [Inject]
    CarFactory(DiContainer diContainer, List<GameObject> carPrefabs)
    {
        _container = diContainer;
        _carPrefabs = carPrefabs;
    }

    public Car Get()
    {
        var car = _container.InstantiatePrefabForComponent<Car>(_carPrefabs[Random.Range(0, _carPrefabs.Count)]);
        car.gameObject.SetActive(false);
        return car;
    }
}