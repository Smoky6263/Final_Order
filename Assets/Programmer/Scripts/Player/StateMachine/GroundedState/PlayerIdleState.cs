using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //IF PLAYER PRESS MOVE INPUT
        if(Context.IsGrounded && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());
        
        //IF PLAYER PRESS "CROUCH" BUTTON
        if(Context.IsGrounded && Context.MovementInput.x == 0 && Context.MovementInput.y < 0)
            SwitchState(Factory.Crouch());

        //IF ON STAIRS
        if (Context.IsGrounded && Context.OnStairs && Context.MovementInput.y != 0)
            SwitchState(Factory.OnStairs());
    }

    public override void EnterState()
    {
        Context.PlayerAnimatorController.OnIdle(true);
    }

    public override void ExitState()
    {
        Context.PlayerAnimatorController.OnIdle(false);

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
