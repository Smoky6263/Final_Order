using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        InitializeSubState();
    }

    public override void CheckSwitchStatesState()
    {
        //IF PLAYER PRESS JUMP
        if (Context.IsGrounded && Context.JumpButtonPressed)
            SwitchState(Factory.Jump());
        //IF PLAYER PRESS MOVE INPUT
        if(Context.IsGrounded && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());
    }

    public override void EnterState()
    {
        Debug.Log("Hello From Idle State");
        CheckSwitchStatesState();
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStatesState();
    }
}
