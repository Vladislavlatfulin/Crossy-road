using UnityEngine;
using UnityEngine.Animations;

public class EnemyAttackPlayer : StateMachineBehaviour
{
    private Player _player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        _player = FindObjectOfType<Player>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(_player.transform);
        var distance = Vector3.Distance(_player.transform.position, animator.transform.position);
        
        if (distance > 3.5f)
            animator.SetBool("isAttacking", false);
    }
}
