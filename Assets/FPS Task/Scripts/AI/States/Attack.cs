

using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Attack : BaseState
{
    float _attackCalldown;
    float _range;
    float timeSinceLastAttack = Mathf.Infinity;


    Health _playerHealth;
    Animator _animator;
    NavMeshAgent _agent;
    CharacterController _characterController;
    protected EnemyAI _enemyAI;
    Animator _anim;

    public Attack(StateMachine stateMachine) : base(stateMachine)
    {
        _enemyAI = context as EnemyAI;
    }

    public override void OnStateEnter()
    {
        Init();
        
    }

   

    public override void OnStateStay()
    {
        AttackPlayer();
    }

    public override void OnStateExit()
    {

    }
    protected virtual void AttackPlayer()
    {
        if (_playerHealth == null || _playerHealth.IsDead()) _enemyAI.SetState(_enemyAI.PatrollingState);

        Vector3 targetPos = new Vector3(_playerHealth.transform.position.x,0,_playerHealth.transform.position.z);
        Vector3 myPos = new Vector3(_enemyAI.transform.position.x, 0, _enemyAI.transform.position.z);
        if (Vector3.Distance(targetPos, myPos) > _range) _enemyAI.SetState(_enemyAI.ChaseState);

        //we can make another class for navmesh attack but i am too lazy
        if (_agent != null)_agent.isStopped = true;
        if(_characterController != null) _characterController.enabled = false;

        _enemyAI.transform.LookAt(_enemyAI.Target);
        _anim.SetFloat("Speed", 0);

        if(timeSinceLastAttack > _attackCalldown)
        {
            _animator.SetTrigger("Attack");
           
            timeSinceLastAttack = 0;
        }
        else
        {
            _animator.ResetTrigger("Attack");
        }
        timeSinceLastAttack += Time.deltaTime;

    }

    protected virtual void Init()
    {
        _attackCalldown = _enemyAI.AttackCalldown;
        _range = _enemyAI.Range;
        _playerHealth = _enemyAI.Target.GetComponent<Health>();
        _animator = _enemyAI.GetComponent<Animator>();
        _agent = _enemyAI.GetComponent<NavMeshAgent>();
        _characterController = _enemyAI.GetComponent<CharacterController>();
        _anim = _enemyAI.GetComponent<Animator>();
    }
}
