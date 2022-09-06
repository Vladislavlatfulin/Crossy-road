using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();
        
        if (_health <= 0)
            Die();
    }

    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }

    private void Die()
    {
       GetComponent<Player>().Die();
    }

    public void ResetHealth()
    {
        _health = totalHealth;
        InitHealth();
    }
}