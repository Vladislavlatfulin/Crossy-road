using DG.Tweening;
using UnityEngine;

public class Car : MonoBehaviour, IObstacle
{
    public void Move(int moveTime)
    {
        transform.DOMove(new Vector3(36, 0, 0), moveTime).SetRelative().OnComplete(OnInvisible);
    }

    private void OnInvisible()
    {
        gameObject.SetActive(false);
    }

    public void CollideWith(ICollidable collidable)
    {
        collidable.CollideWithResult();
    }
}