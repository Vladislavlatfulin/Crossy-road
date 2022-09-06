using System;
using UnityEngine;
using Zenject;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, ICollidable
{
    public event Action CameraChange;
    public event Action PlayerDie;
    [SerializeField] private float _speed = 10f; 
    [Inject] private Camera _camera;
    [Inject] private Joystick _joystick; 
 
    private CharacterController _controller;
    private Animator _animator;
    private float _inputX;
    private float _inputZ;
    private readonly float _gravity = -9.8f;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private bool _cameraChange;
    private Vector3 movement;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    
    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }
 
    private void Update()
    {
        _inputX = _joystick.Horizontal;
        _inputZ = _joystick.Vertical;
        
        if (_cameraChange)
        {
            Vector3 movement = Vector3.zero;

            _inputX = _joystick.Horizontal;
            _inputZ = _joystick.Vertical;
		
            if (_inputX != 0 || _inputZ != 0) {
                movement.x = _inputX * _speed;
                movement.z = _inputZ * _speed;
                movement = Vector3.ClampMagnitude(movement, _speed);
	
                Quaternion tmp = _camera.transform.rotation;
                _camera.transform.eulerAngles = new Vector3(0, _camera.transform.eulerAngles.y, 0);
                movement = _camera.transform.TransformDirection(movement);
                _camera.transform.rotation = tmp;
			
                Quaternion direction = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    direction, 15 * Time.deltaTime);
            }
		
            _animator.SetFloat("Speed", movement.sqrMagnitude);
            movement.y = _gravity;
            movement *= Time.deltaTime;

            _controller.Move(movement);
        }
    }
 
    private void FixedUpdate()
    {
        if (!_cameraChange)
        {
            if (_inputZ >= 0)
            {
                movement = new Vector3(0, 0, _inputZ * _speed);
                _animator.SetFloat(Speed,  _inputZ);
                movement *= Time.fixedDeltaTime;
                _controller.Move(movement);
                if (transform.position.z >= -8)
                {
                    CameraChange?.Invoke();
                    _cameraChange = true;
                    _speed = 4.8f;
                }
            }
        }
    }

    public void CollideWithResult()
    {
        Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        PlayerDie?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IObstacle>(out var obstacle))
            obstacle.CollideWith(this);
    }

    public void RestartPosition()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _speed = 10f;
    }

    public void ResetMove()
    {
        _cameraChange = false;
    }
}