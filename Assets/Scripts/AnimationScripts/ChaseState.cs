using UnityEngine;
using UnityEngine.AI;

public class ChaseState : StateMachineBehaviour
{
    private Player _player;
    private NavMeshAgent _agent;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = FindObjectOfType<Player>();
        
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 2.7f;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.transform.position);
        var distance = Vector3.Distance(_player.transform.position, animator.transform.position);
        
        if (distance > 15)
            animator.SetBool("isChasing", false);

        if (distance < 1.5)
            animator.SetBool("isAttacking", true);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }
}
