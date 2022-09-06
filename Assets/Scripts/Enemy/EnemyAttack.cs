using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float timeToDamage = 1f;
    private float _damageTime;
    private bool _isDamage = true;
    
    private void Start()
    { 
        _damageTime = timeToDamage;
    }
    
    private void Update() 
    { 
        if (!_isDamage) 
        { 
            _damageTime -= Time.deltaTime; 
            if (_damageTime <= 0)
            {
                _damageTime = timeToDamage;
                _isDamage = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && _isDamage)
        {
            playerHealth.ReduceHealth(damage);
            _isDamage = false;
        }
    }
}
