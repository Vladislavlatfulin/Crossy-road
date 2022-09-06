using System;
using UnityEngine;

public interface IInputService
{
    public void AddPointerDownListener(Action<Vector2> listener);
    public void AddPointerMoveListener(Action<Vector2> listener);
    public void AddPointerUpListener(Action<Vector2> listener);
    
    public void RemovePointerDownListener(Action<Vector2> listener);

    public void RemovePointerMoveListener(Action<Vector2> listener);
    public void RemovePointerUpListener(Action<Vector2> listener);
    


}