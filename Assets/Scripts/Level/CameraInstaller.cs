using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    public override void InstallBindings()
    {
        Container.BindInstance(_camera);
        Container.BindInstance(_virtualCamera);
        
        Container.BindInterfacesTo<CamerasState>()
            .AsSingle()
            .NonLazy();
    }
}