
public abstract class BaseState
{

    protected StateMachine context;


    public BaseState(StateMachine stateMachine)
    {
        context = stateMachine;
    }

    public abstract void OnStateEnter();
    

    public abstract void OnStateStay();



    public abstract void OnStateExit();
    

    
}
