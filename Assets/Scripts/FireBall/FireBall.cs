using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private int speed = 10;
    private PlayerAttackController _attackController;
    
    private void Start()
    {
        _attackController = FindObjectOfType<PlayerAttackController>().GetComponent<PlayerAttackController>();
    }
    
    void Update() 
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        
        if(enemyHealth != null && _attackController.IsAttack)
            enemyHealth.TakeDamage(damage);
        
        Destroy(gameObject);
    }
}
