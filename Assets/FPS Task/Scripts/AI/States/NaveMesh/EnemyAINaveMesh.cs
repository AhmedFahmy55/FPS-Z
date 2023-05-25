using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAINaveMesh : EnemyAI
{




    protected override void SetupStates()
    {
        PatrollingState = new PatrolingNavMesh(this);
        ChaseState = new ChaceNaveMesh(this);
        AttackState = new Attack(this);
        DieState = new Die(this);
    }
}
