using UnityEngine;

public class DieState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyHealth>().Die();
    }
}
