using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class StateMachine : MonoBehaviour
{


    private BaseState currentState;


    protected virtual void Start()
    {
        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
        else
        {
            SetState(IntialStat()); 
        }
    }

    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateStay();
        }
        
    }


   
    
    public void SetState(BaseState state) 
    {
        currentState?.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    public abstract BaseState IntialStat();
}
