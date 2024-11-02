using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currenrContext, PlayerStateFactory playerStateFactory) : base(currenrContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void CheckSwitchStatesState()
    {
        //IF JUMP PRESSED
        if (Context.IsGrounded && Context.JumpButtonPressed)
            SwitchState(Factory.Jump());

        //IF PLAYER DO MOVE INPUT
        if(Context.IsGrounded && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {

        if(Context.MovementInput.x != 0)
            SetSubState(Factory.Run());

        else if (Context.MovementInput.x == 0)
            SetSubState(Factory.Idle());
        
    }

    public override void UpdateState()
    {
        CheckSwitchStatesState();
    }

    
    
}
