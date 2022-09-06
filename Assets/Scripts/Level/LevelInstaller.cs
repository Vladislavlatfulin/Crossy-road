using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private List<GameObject> _carPrefabs;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private Transform _door;
    [SerializeField] private PlayerAttackButton _attackButton;
    [SerializeField] private GameOverCanvas _gameOverCanvas;
    [SerializeField] private List<EnemyHealth> _enemyHealths;
    [SerializeField] private ScoreManager _scoreManager;
    public override void InstallBindings()
    {
        BindInputService();
        Container.BindInstance(_scoreManager);
        Container.BindInstance(_enemyHealths);
        Container.BindInstance(_gameOverCanvas);
        Container.BindInstance(_door);
        Container.BindInstance(_attackButton);
        BindWayPoints();
        BindJoystick();
        BindCarFactory();
        BindCarPool();
        BindPlayer();
        BindLevel();
    }

    private void BindInputService()
    {
        Container.BindInterfacesAndSelfTo<InputService>()
            .AsSingle()
            .NonLazy();
    }

    private void BindWayPoints()
    {
        Container.BindInstance(_wayPoints);
    }

    private void BindJoystick()
    {
        Container.BindInstance(_joystick);
    }

    private void BindCarPool()
    {
        Container.BindInterfacesTo<CarPool>()
            .AsSingle()
            .NonLazy();
    }

    private void BindCarFactory()
    {
        Container.BindInstance(_carPrefabs).WhenInjectedInto<IFactory>();
        Container.BindInterfacesTo<CarFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void BindPlayer()
    {
        var playerController =
            Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPosition.position,
                Quaternion.identity, null);

        Container.BindInstance(playerController)
            .AsSingle()
            .NonLazy();
    }

    private void BindLevel()
    {
        Container.BindInterfacesTo<Level>()
            .AsSingle()
            .NonLazy();
    }
}