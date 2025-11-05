using UnityEngine;

public abstract class EnityState 
{
    // la 1 trạng thái, sử dụng StateMachine để đổi trạng thái mới
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;

    public EnityState(Player player ,StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;
        this.player = player;
        anim = player.anim;
        rb = player.rb;
    }
    
    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
    }
    public virtual void Update()
    {
        Debug.Log("Update " + animBoolName);
    }

    public virtual void Exit()
    {
      anim.SetBool(animBoolName, false);
    }

}
