using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        //IF LANDED 
        if (Context.IsGrounded && Context.VerticalVelocity <= 0f)
            SwitchState(Factory.Grounded());
                
        //IF LANDED ON STAIRS
        if (Context.IsFalling && Context.OnStairs)
            SwitchState(Factory.OnStairs());

    }

    public override void EnterState()
    {
        InitializeSubState();

        Context.BodyColl.enabled = true;
        Context.RollInput = false;

        if (Context.OnStairs == false)
            Context.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_Dust, Context.transform.position));

        InitiateJump();

        // Если анимация АТАКИ у торса еще не закончена, тогда только ноги должны проиграть LegsJump анимацию
        // Иначе целиком проигрываем Jump анимацию на двух слоях
        int currentTorsoStateHash = Context.AnimatorController.GetCurrentAnimationStateHash(Context.AnimatorController.TorsoAnimator);

        if (currentTorsoStateHash == Context.AnimatorController.TorsoAttackHash)
            Context.AnimatorController.LegsAnimator.Play(Context.AnimatorController.LegsJump, 0, 0f);
        else
            Context.AnimatorController.DoJump();
    }

    public override void ExitState()
    {

        //LANDED
        Context.IsJumping = false;
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
        CurrentSubState.EnterState();

    }

    public override void UpdateState()
    {
        JumpChecks();
        Jump();
        BumpHead();
        CountTimers();
        CheckSwitchStates();
    }

    private void InitiateJump()
    {
        Context.JumpBufferTimer = Context.MoveStats.JumpBufferTime;
        Context.JumpReleasedDuringBuffer = false;
        
        if (Context.IsJumping == false)
            Context.IsJumping = true;
        
        Context.JumpBufferTimer = 0f;
        Context.VerticalVelocity = Context.MoveStats.InitialJumpVelocity;
    }

    private void JumpChecks()
    {
        
        //WHEN WE RELEASE THE JUMP BUTTON
        if (Context.JumpInput == false)
        {
            Context.JumpReleasedDuringBuffer = true;

            if (Context.IsJumping && Context.VerticalVelocity > 0f)
            {
                if (Context.IsPastApexThreshold)
                {
                    Context.IsPastApexThreshold = false;
                    Context.IsFastFalling = true;
                    Context.FastFallTime = Context.MoveStats.TimeForUpwardsCancel;
                    Context.VerticalVelocity = 0f;
                }
                else
                {
                    Context.IsFastFalling = true;
                    Context.FastFallReleaseSpeed = Context.VerticalVelocity;
                }
            } 
        }

        //INITIATE JUMP WITH BUFFERING AND COYOTE TIME
        if (Context.JumpBufferTimer > 0 && Context.IsJumping && Context.IsGrounded)
        {

            if (Context.JumpReleasedDuringBuffer)
            {
                Context.IsFastFalling = true;
                Context.FastFallReleaseSpeed = Context.VerticalVelocity;
            }
        }

        ////AIR JUMP AFTER COYOTE TIME LAPSED
        //else if (Context.JumpBufferTimer >= 0f && Context.IsFalling)
        //{
        //    Context.IsFalling = false;
        //}
    }
    private void Jump()
    {
        //APPLY GRAVITY WHILE JUMPING
        if (Context.IsJumping)
        {
            //CHECK FOR HEAD BUMP
            if (Context.BumpedHead)
            {
                Context.IsFastFalling = true;
            }

            //GRAVITY ON ASCENDING
            if (Context.VerticalVelocity >= 0)
            {
                //APEX CONTROLS
                Context.ApexPoint = Mathf.InverseLerp(Context.MoveStats.InitialJumpVelocity, 0f, Context.VerticalVelocity);

                if (Context.ApexPoint > Context.MoveStats.ApexThreshold)
                {
                    if (Context.IsPastApexThreshold == false)
                    {
                        Context.IsPastApexThreshold = true;
                        Context.TimePastApexThreshold = 0f;
                    }

                    if (Context.IsPastApexThreshold)
                    {
                        Context.TimePastApexThreshold += Time.fixedDeltaTime;

                        if (Context.TimePastApexThreshold < Context.MoveStats.ApexHangTime)
                        {
                            Context.VerticalVelocity = 0f;
                        }
                        else
                        {
                            Context.VerticalVelocity = -0.01f;
                        }
                    }
                }

                //GRAVITY ON ASCENDING BUT NOT PAST APEX TRESHOLD
                else
                {
                    Context.VerticalVelocity += Context.MoveStats.Gravity * Time.deltaTime;
                    if (Context.IsPastApexThreshold)
                    {
                        Context.IsPastApexThreshold = false;
                    }
                }
            }

            //GRAVITY ON DESCENDING 
            else if (Context.IsFastFalling == false)
            {
                Context.VerticalVelocity += Context.MoveStats.Gravity * Context.MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if (Context.VerticalVelocity < 0f)
            {

                if (Context.IsFalling == false)
                {
                    Context.IsFalling = true;
                }
            }
        }

        //JUMP CUT
        if (Context.IsFastFalling)
        {
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

        //NORMAL GRAVITY WHILE FALLING
        if (Context.IsGrounded == false && Context.IsJumping == false)
        {
            if (Context.IsFalling == false)
            {
                Context.IsFalling = true;
            }

            Context.VerticalVelocity += Context.MoveStats.Gravity * Time.deltaTime;
        }

        //CLAMP FALLS SPEED
        Context.VerticalVelocity = Mathf.Clamp(Context.VerticalVelocity, -Context.MoveStats.MaxFallSpeed, 50f);

        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);

        if(Context.VerticalVelocity < 0f)
            Context.IsJumping = false;

    }
    private void BumpHead()
    {
        Vector2 boxCastOrigin = new Vector2(Context.FeetColl.bounds.center.x, Context.BodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(Context.FeetColl.bounds.size.x * Context.MoveStats.HeadWidth, Context.MoveStats.HeadDetectionRayLength);

        Context.HeadHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, Context.MoveStats.HeadDetectionRayLength, Context.MoveStats.HeadBumpSurfaceLayer);

        if (Context.HeadHit.collider != null)
            Context.BumpedHead = true;

        else { Context.BumpedHead = false; }
    }

    #region Timers
    private void CountTimers()
    {
        Context.JumpBufferTimer -= Time.deltaTime;
    }

    #endregion
    public override void PlayerOnAttackAnimationComplete()
    {

    }
}
