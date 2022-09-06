using System.Collections.Generic;
using System.Linq;
using Zenject;

public class CarPool : ICarPool, IInitializable
{
    private IFactory _factory;
    private const int NUMBER_CAR = 30;
    private List<Car> _cars;

    [Inject]
    public CarPool(IFactory factory)
    {
        _factory = factory;
    }

    public Car Get()
    {
        var freeCars = _cars.Where(c => c.gameObject.activeInHierarchy == false).ToArray();
        return freeCars.First();
    }

    public void Initialize()
    {
        _cars = new List<Car>();

        for (int i = 0; i < NUMBER_CAR; i++)
        {
            var car = _factory.Get();
            _cars.Add(car);
        }
    }
}