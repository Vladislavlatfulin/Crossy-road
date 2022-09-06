using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class Level : IInitializable, ILateDisposable
{
    [Inject] private Player _player;
    [Inject] private Transform _door;
    [Inject] private GameOverCanvas _gameOverCanvas;
    [Inject] private List<EnemyHealth> _enemyHealths;
    [Inject] private Camera _camera;
    [Inject] private CinemachineVirtualCamera _virtualCamera;
    [Inject] private ScoreManager _scoreManager;

    private Vector3 _startCameraPosition = new Vector3(5.4f, 10.65f, -53.36f);
    private Vector3 _startCameraRotation = new Vector3(23.95f, -35f, 0f);

    public void Initialize()
    {
        _player.CameraChange += CloseDoor;
        _player.PlayerDie += ActivateLoseCanvas;
        _gameOverCanvas.RestartLevel += RestartLevel;
    }

    private void RestartLevel()
    {
        _player.RestartPosition();
        _player.gameObject.SetActive(true);
        _gameOverCanvas.gameObject.SetActive(false);

        _player.GetComponent<PlayerHealth>().ResetHealth();
        _player.ResetMove();
        _door.gameObject.SetActive(false);
        ResetCamera();
        foreach (var enemy in _enemyHealths)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
            }
            enemy.RestartHealth();
        }
        _scoreManager.resetCountEnemy();
    }

    private void ResetCamera()
    {
        _virtualCamera.gameObject.SetActive(false);
        
        _virtualCamera.LookAt = null;
        _virtualCamera.Follow = null;
        _camera.GetComponent<CinemachineBrain>().enabled = false;
        _camera.transform.position = _startCameraPosition;
        _camera.transform.eulerAngles = _startCameraRotation;
    }

    private void ActivateLoseCanvas()
    {
        _gameOverCanvas.SetText("You Lose!");
        _gameOverCanvas.gameObject.SetActive(true);
    }

    public void LateDispose()
    {
        _player.CameraChange -= CloseDoor;
        _player.PlayerDie -= ActivateLoseCanvas;
        _gameOverCanvas.RestartLevel -= RestartLevel;
    }

    private void CloseDoor()
    {
        _door.gameObject.SetActive(true);
    }
}