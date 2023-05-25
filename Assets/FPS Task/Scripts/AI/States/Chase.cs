using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState
{
    float _speed;
    float _range;
    float _chaceDistance;

    protected EnemyAI _enemyAI;
    protected Transform _target;
    CharacterController _characterController;

    public Chase(StateMachine stateMachine) : base(stateMachine)
    {
        _enemyAI = context as EnemyAI;
    }

    public override void OnStateEnter()
    {
        Init();
    }

    protected virtual void Init()
    {
        _speed = _enemyAI.Speed;
        _range = _enemyAI.Range;
        _chaceDistance = _enemyAI.ChaceDistance;
        _target = _enemyAI.Target;
        _characterController = _enemyAI.GetComponent<CharacterController>();
    }

    public override void OnStateStay()
    {
        HandleChacing();
    }

    public override void OnStateExit()
    {

    }

    void HandleChacing()
    {

        
        Vector3 targetV = new Vector3(_target.position.x, 0, _target.position.z);
        Vector3 myPos = new Vector3(_enemyAI.transform.position.x, 0, _enemyAI.transform.position.z);
        if (Vector3.Distance(targetV, myPos) <= _range)
        {
            _enemyAI.SetState(_enemyAI.AttackState);
        }

        if (Vector3.Distance(targetV, myPos) > _chaceDistance)
        {
            _enemyAI.SetState(_enemyAI.PatrollingState);
        }

        ChaceTarget();

    }

    protected virtual void ChaceTarget()
    {
        _characterController.enabled = true;
        Vector3 dire = (_target.position - _enemyAI.transform.position).normalized;
        _enemyAI.transform.LookAt(_target);
        _characterController.Move(_speed * Time.deltaTime * dire);
    }
}
