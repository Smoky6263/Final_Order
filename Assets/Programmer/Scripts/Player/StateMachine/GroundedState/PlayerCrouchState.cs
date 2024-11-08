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
        
        //IF JUMP PRESS AND CROUCH PRESS DO DROP FROM PLATFORM
        if (Context.JumpButtonPressed && Context.MovementInput.y < 0 && Context.CurrentPTP != null)
        {
            Context.CurrentPTP.GetComponent<PassTroughPlatform>().TurnOffCollision(Context.BodyColl, Context.FeetColl);
            Context.CurrentPTP = null;
            Context.CoyoteTimer = -1f;
            SwitchState(Factory.Fall());
        }
    }

    public override void EnterState()
    {
        Context.AnimatorController.OnCrouch(true);
        Context.OnCrouch = true;

        Context.MovementVelocity = Vector2.zero;
        Context.RigidBody.velocity = new Vector2(0f, Context.RigidBody.velocity.y);
    }

    public override void ExitState()
    {
        Context.OnCrouch = false;
        Context.AnimatorController.OnCrouch(false);
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
