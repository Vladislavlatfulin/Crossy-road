using Cinemachine;
using UnityEngine;
using Zenject;

public class CamerasState : IInitializable, ILateDisposable
{
    [Inject] private Player _player;
    [Inject] private Camera _camera;
    [Inject] private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBrain _brain;

    public void Initialize()
    {
        _brain = _camera.GetComponent<CinemachineBrain>();
        _player.CameraChange += ChangeCamera;
    }

    private void ChangeCamera()
    {
        _virtualCamera.gameObject.SetActive(true);
        _virtualCamera.Follow = _player.transform;
        _virtualCamera.LookAt = _player.transform;
       _brain.enabled = true;
    }

    public void LateDispose()
    {
        _player.CameraChange -= ChangeCamera;
    }
}