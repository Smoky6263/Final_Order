using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        //IF GROUNDED AND UNPRESS "CROUCH" BUTTON
        if (Context.IsGrounded && Context.MovementInput.y >= 0 && Context.MovementInput.x == 0)
        {
            SwitchState(Factory.Idle());
        }

        //IF GROUNDED AND UNPRESS "CROUCH", AND PRESS "RUN" BUTTONS
        if (Context.IsGrounded && Context.MovementInput.y >= 0 && Context.MovementInput.x != 0)
        {
            Context.AnimatorController.OnRun();
            SwitchState(Factory.Run());
        }
        
        //IF JUMP PRESS AND CROUCH PRESS DO DROP FROM PLATFORM
        if (Context.JumpInput && Context.MovementInput.y < 0 && Context.CurrentPTP != null)
        {
            Context.CurrentPTP.GetComponent<PassTroughPlatform>().TurnOffCollision(Context.BodyColl, Context.FeetColl);
            Context.CurrentPTP = null;
            Context.CoyoteTimer = -1f;
            SwitchState(Factory.Fall());
        }

        //DO ROLL WHEN PRESS ROLL BUTTON
        if (Context.IsGrounded  && Context.RollInput == true)
            SwitchState(Factory.Roll());
        
        //WHEN WE PRESS THE JUMP BUTTON
        if (Context.JumpInput && (Context.CoyoteTimer >= 0 && Context.JumpBufferTimer >= 0) && Context.IsGrounded)
            SwitchState(Factory.Jump());
    }

    public override void EnterState()
    {
        InitializeSubState();

        Context.BodyColl.enabled = true;
        Context.AnimatorController.OnCrouch();
        Context.OnCrouch = true;
        Context.JumpInput = false;

        Context.MovementVelocity = Vector2.zero;
        Context.RigidBody.velocity = new Vector2(0f, Context.RigidBody.velocity.y);
    }

    public override void ExitState()
    {
        Context.OnCrouch = false;
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
