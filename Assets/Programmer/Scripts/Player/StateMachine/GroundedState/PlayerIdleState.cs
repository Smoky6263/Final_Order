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
        if((Context.IsGrounded || Context.OnStairs) && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());
        
        //IF PLAYER PRESS "CROUCH" BUTTON
        if(Context.IsGrounded && Context.OnStairs == false && Context.MovementInput.x == 0 && Context.MovementInput.y < 0)
            SwitchState(Factory.Crouch());

        //DO ROLL WHEN PRESS ROLL BUTTON
        if ((Context.IsGrounded || Context.OnStairs) && Context.MovementInput.x == 0f && Context.RollInput == true)
            SwitchState(Factory.Roll());
    }

    public override void EnterState()
    {
        Context.AnimatorController.OnIdle(true);
    }

    public override void ExitState()
    {
        Context.AnimatorController.OnIdle(false);

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
