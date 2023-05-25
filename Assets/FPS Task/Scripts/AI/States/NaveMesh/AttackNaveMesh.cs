using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackNaveMesh : Attack
{
    NavMeshAgent _agent;
    public AttackNaveMesh(StateMachine stateMachine) : base(stateMachine)
    {
    }


    protected override void AttackPlayer()
    {
        _agent.isStopped = true;
        base.AttackPlayer();
    }

    protected override void Init()
    {
        base.Init();
        _agent = _enemyAI.GetComponent<NavMeshAgent>();
    }
}
