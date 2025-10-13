using UnityEngine;

public class StateMachine 
{
    //Máy chứa xử lý trang thái hiện tại
    public EnityState currentState { get; private set; }

    public void Initialize (EnityState startState)
    {
        this.currentState = startState;
        currentState.Enter ();
    }
    public void ChangeState(EnityState newState)
    {
        currentState.Exit ();
        currentState = newState;
        currentState.Enter ();
    }
}
