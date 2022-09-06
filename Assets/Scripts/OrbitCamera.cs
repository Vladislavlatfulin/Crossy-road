using UnityEngine;
using Zenject;

public class OrbitCamera : MonoBehaviour
{
    [Inject] private Player _player;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _player.gameObject.transform.position;
    }

    private void Update()
    {
        transform.position = _player.transform.position + _offset;
    }
}