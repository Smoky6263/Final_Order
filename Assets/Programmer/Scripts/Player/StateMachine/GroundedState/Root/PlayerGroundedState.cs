using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //WHEN WE PRESS THE JUMP BUTTON
        if (Context.JumpButtonPressed && Context.CoyoteTimer >= 0)
        {
            Context.JumpBufferTimer = Context.MoveStats.JumpBufferTime;
            Context.JumpReleasedDuringBuffer = false;

            if (Context.JumpBufferTimer >= 0)
                SwitchState(Factory.Jump());
        }

        //IF PLAYER FALL FROM PLATFORM
        if (Context.OnStairs == false && Context.IsGrounded == false && Context.JumpButtonPressed == false && Context.CoyoteTimer <= 0)
            SwitchState(Factory.Fall());
    }

    public override void EnterState()
    {
        Context.VerticalVelocity = Physics2D.gravity.y;
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {
        if(Context.IsGrounded && Context.MovementVelocity.x != 0)
            SetSubState(Factory.Run());
        
        if (Context.IsGrounded && Context.MovementVelocity.x == 0)
            SetSubState(Factory.Idle());

        if(Context.OnStairs && Context.MovementInput != Vector2.zero)
            SetSubState(Factory.OnStairs());
    }

    public override void UpdateState()
    {
        Fall();
        CheckSwitchStates();
    }

    private void Fall()
    {
        if (Context.OnStairs) return;
        //CLAMP FALLS SPEED
        Context.VerticalVelocity = Mathf.Clamp(Context.VerticalVelocity, -Context.MoveStats.MaxFallSpeed, 50f);
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);
    }

}