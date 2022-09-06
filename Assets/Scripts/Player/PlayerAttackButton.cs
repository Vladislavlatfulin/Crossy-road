using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackButton : MonoBehaviour
{
    public event Action OnClickAttackButton;
    [SerializeField] private Button _button;
    
    private void OnEnable() => _button.onClick.AddListener(OnClickAttack);
    private void OnDisable() => _button.onClick.RemoveListener(OnClickAttack);
    

    private void OnClickAttack()
    {
        OnClickAttackButton?.Invoke();
    }
}
