using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        //IF GROUNDED AND UNPRESS "CROUCH" BUTTON
        if (Context.IsGrounded && Context.MovementInput.y >= 0 && Context.MovementInput.x == 0 && Context.BumpedHead == false)
        {
            SwitchState(Factory.Idle());
        }

        //IF GROUNDED AND UNPRESS "CROUCH", AND PRESS "RUN" BUTTONS
        if (Context.IsGrounded && Context.MovementInput.y >= 0 && Context.MovementInput.x != 0 && Context.BumpedHead == false)
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
        if (Context.JumpInput && (Context.CoyoteTimer >= 0 && Context.JumpBufferTimer >= 0) && Context.IsGrounded && Context.BumpedHead == false)
            SwitchState(Factory.Jump());
    }

    public override void EnterState()
    {
        InitializeSubState();

        Context.BodyColl.enabled = Context.BumpedHead ? false : true;
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

    public override void PlayerOnAttackAnimationComplete()
    {

    }

    public override void UpdateState()
    {
        TurnCheck(Context.MovementInput);
        CheckSwitchStates();
    }
    private void TurnCheck(Vector2 moveInput)
    {
        if (Context.IsFacingRight && moveInput.x < 0)
            Turn(false);

        else if (Context.IsFacingRight == false && moveInput.x > 0)
            Turn(true);
    }
    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            Context.IsFacingRight = true;
            Context.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(Context.WeaponController.DamageBox_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);

            Context.CameraFollowObject.CallTurn();

        }
        else
        {
            Context.IsFacingRight = false;
            Context.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(-Context.WeaponController.DamageBox_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);

            Context.CameraFollowObject.CallTurn();

        }
    }
}
