using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolState : StateMachineBehaviour
{
    private List<Transform> _wayPoints;
    private Player _player;
    private NavMeshAgent _agent;
    private float _time;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var obj = GameObject.FindGameObjectWithTag("waypoints");
        _wayPoints = new List<Transform>();
        foreach (Transform o in obj.transform)
        {
            _wayPoints.Add(o);
        }
        
        _player = FindObjectOfType<Player>();
        _time = 0;
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 1.5f;
        _agent.SetDestination(_wayPoints[Random.Range(0, _wayPoints.Count)].position);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_wayPoints == null)
        {
            Debug.Log("null");
        }
        if (_agent.remainingDistance <= _agent.stoppingDistance)
            _agent.SetDestination(_wayPoints[Random.Range(0, _wayPoints.Count)].position);

        _time += Time.deltaTime;
        if (_time >= 10)
            animator.SetBool("isPatrolling", false);
        
        
        var distance = Vector3.Distance(_player.transform.position, animator.transform.position);
        if (distance < 8)
            animator.SetBool("isChasing", true);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
        _wayPoints.Clear();
    }
}
