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
        if (Context.IsFalling && Context.IsGrounded && Context.VerticalVelocity <= 0f)
            SwitchState(Factory.Grounded());

        //IF GROUND ON STAIRS
        if (Context.IsFalling && Context.OnStairs)
            SwitchState(Factory.OnStairs());
    }

    public override void EnterState()
    {
        //------------------------------------------------------
        //DO JUMP ANIMATION
        //------------------------------------------------------
        Context.RollInput = false;
        Context.JumpBufferTimer = -1f;


        if (Context.IsFalling == false && Context.IsJumping == false)
            Context.IsFalling = true;

        if (Context.IsFastFalling)
        {
            Context.FastFallTime = Context.MoveStats.TimeForUpwardsCancel;
            Context.VerticalVelocity = 0f;
        }

    }

    public override void ExitState()
    {
        //LANDED
        Context.IsFalling = false;
        Context.IsFastFalling = false;

        Context.FastFallTime = 0f;
        Context.IsPastApexThreshold = false;

        Context.VerticalVelocity = Physics2D.gravity.y;

    }

    public override void InitializeSubState()
    {
        //IF PLAYER FALLING AND RUN
        SetSubState(Factory.FallingRun());
    }

    public override void UpdateState()
    {
        if(Context.IsFastFalling == false)
            Fall();

        if (Context.IsFastFalling == true)
            FastFall();

        //CLAMP FALLS SPEED
        Context.VerticalVelocity = Mathf.Clamp(Context.VerticalVelocity, -Context.MoveStats.MaxFallSpeed, 50f);
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);

        CountTimers();
        CheckSwitchStates();
    }

    private void Fall()
    {
        Context.VerticalVelocity += Context.MoveStats.Gravity * Time.deltaTime;
    }

    private void FastFall()
    {
        //JUMP CUT
        if (Context.FastFallTime >= Context.MoveStats.TimeForUpwardsCancel)
        {
            Context.VerticalVelocity += Context.MoveStats.Gravity * Context.MoveStats.GravityOnReleaseMultiplier * Time.deltaTime;
        }
        else if (Context.FastFallTime < Context.MoveStats.TimeForUpwardsCancel)
        {
            Context.VerticalVelocity = Mathf.Lerp(Context.FastFallReleaseSpeed, 0f, (Context.FastFallTime / Context.MoveStats.TimeForUpwardsCancel));
        }

        Context.FastFallTime += Time.deltaTime;
    }

    #region Timers
    private void CountTimers()
    {
        Context.JumpBufferTimer -= Time.deltaTime;
    }
    #endregion
}
