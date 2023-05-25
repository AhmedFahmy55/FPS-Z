using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAI : StateMachine
{
    [Header("General Setting")]
    [SerializeField] float _damage;
    public float Damage { get { return _damage; }}

    [SerializeField] float _range;
    public float Range { get { return _range; }}

    [SerializeField] float _attackCalldown;
    public float AttackCalldown { get { return _attackCalldown;}}

    [SerializeField] float _chaceDistance;
    public float ChaceDistance { get { return _chaceDistance; }}

    [Header("Patrolling Setting")]

    [SerializeField] float _speed;
    public float Speed { get { return _speed; } }

    [SerializeField] float _stopingDistance;
    public float StopingDistance { get { return _stopingDistance; } }

    [SerializeField] float _waitingTime;
    public float WaitingTime { get { return _waitingTime; } }


    public Patroling PatrollingState { get; protected set; }
    public Chase ChaseState { get; protected set; }
    public Attack AttackState { get; protected set; }
    public Die DieState { get; protected set; }
    public Transform Target { get; private set; }








    private void Awake()
    {
        Target = GameObject.FindWithTag("Player").transform;
        SetupStates();
    }

    protected virtual void SetupStates()
    {
        PatrollingState = new Patroling(this);
        ChaseState = new Chase(this);
        AttackState = new Attack(this);
        DieState = new Die(this);
    }

    protected override void Start()
    {        
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
       
    }




    //animation event
    public void DamagePlayer()
    {
        Health player = Target.GetComponent<Health>();
        if (player != null)
            player.TakeDamage(Damage,gameObject);
    }

    public void AttackPlayer()
    {
        SetState(AttackState);
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, ChaceDistance);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, Range);
        
    }

    public override BaseState IntialStat()
    {
        return PatrollingState;
    }
}
