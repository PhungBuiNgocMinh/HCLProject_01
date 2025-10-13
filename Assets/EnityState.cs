using UnityEngine;

public class EnityState 
{
    // 1 trạng thái, sử dụng StateMachine để đổi trạng thái mới
    protected StateMachine stateMachine;
    protected string stateName;
    public EnityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        Debug.Log("Ender "+ stateName);
    }
    public virtual void Update()
    {
        Debug.Log("Update " + stateName);
    }

    public virtual void Exit()
    {
        Debug.Log("Exit " + stateName);
    }

}
