using UnityEngine;

public class PlayerAttackState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var _attackController = animator.GetComponent<PlayerAttackController>();
        _attackController.FinishAttack();    
    }
}