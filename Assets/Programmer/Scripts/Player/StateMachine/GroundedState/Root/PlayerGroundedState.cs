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
        if (Context.JumpInput && ( Context.CoyoteTimer >= 0 && Context.JumpBufferTimer >= 0 ) && Context.IsGrounded && Context.OnCrouch == false)
        {
            SwitchState(Factory.Jump());
        }

        //IF ON STAIRS
        if (Context.OnStairs && Context.MovementInput.y != 0)
            SwitchState(Factory.OnStairs());

        //IF PLAYER FALL
        if (Context.OnStairs == false && Context.IsGrounded == false && Context.CoyoteTimer <= 0)
            SwitchState(Factory.Fall());
        
    }

    public override void EnterState()
    {
        Context.CoyoteTimer = Context.MoveStats.JumpCoyoteTime;
        Context.VerticalVelocity = Physics2D.gravity.y;
        
        if(Context.OnStairs == false)
            Context.VFXManager.SpawnDustParticles(Context.transform.position);

        if (Context.OnCrouch)
            Context.OnCrouch = false;
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

        if(Context.OnStairs && Context.MovementInput.y != 0f)
            SetSubState(Factory.OnStairs());
    }

    public override void UpdateState()
    {
        Fall();
        CountTimers();
        CheckSwitchStates();
    }

    private void Fall()
    {
        if (Context.OnStairs) return;

        //CLAMP FALLS SPEED
        Context.VerticalVelocity = Mathf.Clamp(Context.VerticalVelocity, -Context.MoveStats.MaxFallSpeed, 50f);
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);
    }

    private void CountTimers()
    {
        if (Context.IsGrounded == false && Context.OnStairs == false)
            Context.CoyoteTimer -= Time.deltaTime;
    }

}
