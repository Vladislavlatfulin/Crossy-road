using System;
using UnityEngine;
using Zenject;

public class InputService : ILateTickable, IInputService
{
    private event Action<Vector2> _pointerDownEventHandler;
    private event Action<Vector2> _pointerMoveEventHandler;
    private event Action<Vector2> _pointerUpEventHandler;   
    
    public void LateTick()
    {
        if (Input.touchCount == 0)
            return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _pointerDownEventHandler?.Invoke(touch.position);
                    break;

                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    _pointerMoveEventHandler?.Invoke(touch.position);
                    break;

                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    _pointerUpEventHandler?.Invoke(touch.position);
                    break;
            }
        }
    }

    public void AddPointerDownListener(Action<Vector2> listener) => _pointerDownEventHandler += listener;

    public void AddPointerMoveListener(Action<Vector2> listener) => _pointerMoveEventHandler += listener;

    public void AddPointerUpListener(Action<Vector2> listener) => _pointerUpEventHandler += listener;

    public void RemovePointerDownListener(Action<Vector2> listener) => _pointerDownEventHandler -= listener;

    public void RemovePointerMoveListener(Action<Vector2> listener) => _pointerMoveEventHandler -= listener;

    public void RemovePointerUpListener(Action<Vector2> listener) => _pointerUpEventHandler -= listener;
}