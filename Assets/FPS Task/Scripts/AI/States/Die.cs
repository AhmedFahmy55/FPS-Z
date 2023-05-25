using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Die : BaseState
{

    EnemyAI _controller;
    public Die(StateMachine stateMachine) : base(stateMachine)
    {
        _controller = context as EnemyAI;
    }

    public override void OnStateEnter()
    {
        Init();
    }

    private void Init()
    {
        _controller.GetComponent<Animator>().SetTrigger("Die");
        _controller.GetComponent<CapsuleCollider>().enabled = false;
        if ( _controller.TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;
        if (_controller.TryGetComponent(out CharacterController controller)) controller.enabled = false;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateStay()
    {
        
    }
}
