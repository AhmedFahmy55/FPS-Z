using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolingNavMesh : Patroling
{

    NavMeshAgent _agent;





    public PatrolingNavMesh(StateMachine stateMachine) : base(stateMachine)
    {
    }


    protected override void Init()
    {
        base.Init();
        _agent = _enemyAI.GetComponent<NavMeshAgent>();
        _agent.speed = _speed;

    }
    protected override void HandleMovement(Vector3 point)
    {
        
       
        if (!CanMoveToPoint(point))
        {
            souldCalculateNewPoint = true;
            return;
        }
        
        
        
        if (Vector3.Distance(point, _enemyAI.transform.position) < _stoppingDistance)
        {
            _agent.enabled = false;
            _animator.SetFloat("Speed", 0);


        }
        else
        {
            _agent.enabled = true;
            _agent.SetDestination(point);
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }
    }

    private bool CanMoveToPoint(Vector3 point)
    {
        NavMeshPath path = new NavMeshPath();
        if (!NavMesh.CalculatePath(_agent.transform.position, point, NavMesh.AllAreas, path)) return false;
        if (path.status != NavMeshPathStatus.PathComplete) return false;
        return true;
    }
}
