using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //IF GROUNDED AND UNPRESS "CROUCH" BUTTON
        if(Context.IsGrounded && Context.MovementInput.y >= 0 && Context.MovementInput.x == 0)
            SwitchState(Factory.Idle());

        //IF GROUNDED AND UNPRESS "CROUCH", AND PRESS "RUN" BUTTONS
        if (Context.IsGrounded && Context.MovementInput.y >= 0 && Context.MovementInput.x != 0)
            SwitchState(Factory.Run());
    }

    public override void EnterState()
    {
        Context.PlayerAnimatorController.OnCrouch(true);
        Context.RigidBody.velocity = new Vector2(0f, Context.RigidBody.velocity.y);
    }

    public override void ExitState()
    {
        Context.PlayerAnimatorController.OnCrouch(false);
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
