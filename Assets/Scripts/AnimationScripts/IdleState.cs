using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    private Player _player;
    private float _time;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = FindObjectOfType<Player>();
        _time = 0;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _time += Time.deltaTime;

        if (_time > 5)
            animator.SetBool("isPatrolling", true);
        
        var distance = Vector3.Distance(_player.transform.position, animator.transform.position);
        
        if (distance < 6)
            animator.SetBool("isAttacking", true);
    }
}
