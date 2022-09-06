using UnityEngine;
using Zenject;

public class PlayerAttackController : MonoBehaviour
{
    private bool _isAttack = false;
    public bool IsAttack => _isAttack;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private GameObject _fireBallPrefab;
    [Inject] private PlayerAttackButton _attackButton;
    private GameObject _fireBall;

    private void OnEnable() => _attackButton.OnClickAttackButton += Attack;
    
    private void OnDisable() => _attackButton.OnClickAttackButton -= Attack;

    public void FinishAttack()
    {
        _isAttack = false;
    }
    
    public void Attack()
    {
        _isAttack = true;
        _playerAnimator.SetTrigger("Attack");
        if (!_fireBall)
        {
            _fireBall = Instantiate(_fireBallPrefab);
            _fireBall.transform.position =
                transform.TransformPoint(new Vector3(0, 3f,2) );
            _fireBall.transform.rotation = transform.rotation;
        }
    }
}