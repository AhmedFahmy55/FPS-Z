using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patroling : BaseState
{

    protected float _speed;
    protected float _stoppingDistance ;
    protected float _chaceDistance;
    protected float _w8ingTime;
    protected float _delta = 0;
    float rad = 10;
    protected bool souldCalculateNewPoint = true;
    Vector3 targetPoint;

    CharacterController _characterController;
    protected Transform _player;
    protected Animator _animator;
    protected EnemyAI _enemyAI;
    public Patroling(StateMachine stateMachine) : base(stateMachine)
    {
        _enemyAI = context as EnemyAI;
    }

    public override void OnStateEnter()
    {
        Init();
    }

    

    public override void OnStateStay()
    {
        PatrolBehaviour();
    }

    public override void OnStateExit()
    {

    }
    protected virtual void Init()
    {
        _speed = _enemyAI.Speed;
        _stoppingDistance = _enemyAI.StopingDistance;
        _chaceDistance = _enemyAI.ChaceDistance;
        _w8ingTime = _enemyAI.WaitingTime;
        _delta = _w8ingTime;
        _player = _enemyAI.Target;
        _characterController = _enemyAI.GetComponent<CharacterController>();
        _animator = _enemyAI.GetComponent<Animator>();
    }
    void PatrolBehaviour()
    {
        
        if (Vector3.Distance(_enemyAI.transform.position, _player.position) < _chaceDistance &&
            !_player.GetComponent<Health>().IsDead())
        {
            _enemyAI.SetState(_enemyAI.ChaseState);
            return;
        }

        if (souldCalculateNewPoint)
        {
            targetPoint = CalculateRandomPos();
            souldCalculateNewPoint = false;
        } 
        if (Vector3.Distance(targetPoint, _enemyAI.transform.position) < _stoppingDistance)
        {
            _animator.SetFloat("Speed", 0);
            if (_characterController != null) _characterController.enabled = false;

            _delta -= Time.deltaTime;
            if (_delta <= 0)
            {
                souldCalculateNewPoint = true;
                _delta = _w8ingTime;
            }
        }
        else
        {
            if (_characterController != null)
            {
                _characterController.enabled = true;
                _animator.SetFloat("Speed", _characterController.velocity.magnitude);
            }

        }

        HandleMovement(targetPoint);


    }

    private Vector3 CalculateRandomPos()
    {
        return _enemyAI.transform.position + new Vector3
               (Random.insideUnitCircle.x * rad,0, Random.insideUnitCircle.x * rad);
    }

    protected virtual void HandleMovement(Vector3 targetPoint)
    {
        Vector3 Dire = (targetPoint - _enemyAI.transform.position).normalized;
        _enemyAI.transform.LookAt(targetPoint);
       if(_characterController.enabled) _characterController.Move(_speed * Time.deltaTime * Dire);


    }
    
}
