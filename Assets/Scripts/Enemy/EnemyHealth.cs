using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private ScoreManager _scoreManager;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("takeDamage");
        InitHealth();
        if(_health <= 0)
        {
            _animator.SetTrigger("Die");
        }
    }
    
    private void InitHealth()
    {
        _healthBar.value = _health / totalHealth;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _scoreManager.DecreaseScore();
    }

    public void RestartHealth()
    {
        _health = totalHealth;
        InitHealth();
    }
}