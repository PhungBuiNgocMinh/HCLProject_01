using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (player.grounDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
