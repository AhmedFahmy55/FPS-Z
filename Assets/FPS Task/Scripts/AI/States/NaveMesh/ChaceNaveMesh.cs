using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaceNaveMesh : Chase
{
    NavMeshAgent _agent;
    Animator _animator;


    public ChaceNaveMesh(StateMachine stateMachine) : base(stateMachine)
    {
    }





    protected override void Init()
    {
        base.Init();
        _agent = _enemyAI.GetComponent<NavMeshAgent>();
        _animator = _enemyAI.GetComponent<Animator>();
        _agent.enabled = true;
    }


    protected override void ChaceTarget()
    {
        
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }
}
