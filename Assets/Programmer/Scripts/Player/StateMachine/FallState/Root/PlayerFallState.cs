using UnityEngine;

class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //IF LANDED
        if ((Context.IsJumping || Context.IsFalling) && Context.IsGrounded && Context.VerticalVelocity <= 0f)
            SwitchState(Factory.Grounded());

        //IF GROUND ON STAIRS
        if ((Context.IsJumping || Context.IsFalling) && Context.OnStairs && Context.MovementInput.y != 0f)
            SwitchState(Factory.Grounded());
    }

    public override void EnterState()
    {
        //------------------------------------------------------
        //DO JUMP ANIMATION
        //------------------------------------------------------

        if (Context.IsFalling == false && Context.IsJumping == false)
            Context.IsFalling = true;

    }

    public override void ExitState()
    {
        //LANDED
        Context.IsJumping = false;
        Context.IsFalling = false;
        Context.IsFastFalling = false;

        Context.FastFallTime = 0f;
        Context.JumpBufferTimer = 0f;
        Context.IsPastApexThreshold = false;

        Context.JumpButtonPressed = false;
        Context.VerticalVelocity = Physics2D.gravity.y;

    }

    public override void InitializeSubState()
    {
        //IF PLAYER FALLING AND RUN
        SetSubState(Factory.FallingRun());
    }

    public override void UpdateState()
    {
        Context.VerticalVelocity += Context.MoveStats.Gravity * Time.deltaTime;
        Fall();
        CheckSwitchStates();
    }

    private void Fall()
    {
        //CLAMP FALLS SPEED
        Context.VerticalVelocity = Mathf.Clamp(Context.VerticalVelocity, -Context.MoveStats.MaxFallSpeed, 50f);
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);
    }
}
