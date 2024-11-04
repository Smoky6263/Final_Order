using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //IF IDLE
        if(Context.IsGrounded && Context.MovementVelocity.x == 0)
            SwitchState(Factory.Idle());
        
        //IF PLAYER FALL FROM PLATFORM
        if (Context.IsGrounded == false && Context.JumpButtonPressed == false)
            SwitchState(Factory.Fall());
    }

    public override void EnterState()
    {
        Debug.Log("Enter RunState");
        TurnCheck(Context.MovementInput);
    }

    public override void ExitState()
    {
        Debug.Log("Exit RunState");
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Debug.Log("Update RunState");

        Move();
        CheckSwitchStates();
    }

    private void Move()
    {
        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(Context.MovementInput.x, 0f) * Context.MoveStats.MaxRunSpeed;

        TurnCheck(Context.MovementInput);

        Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, targetVelocity, Context.GroundAcceleration * Time.fixedDeltaTime);
        Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);
        
        //IF MoveInput == 0 => SwitchState to BRING TO IDLE
        if (Context.MovementInput == Vector2.zero)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, Context.GroundDeceleration * Time.fixedDeltaTime);
            Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);
        }
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
            Context.SpriteRenderer.flipX = false;
        }
        else
        {
            Context.IsFacingRight = false;
            Context.SpriteRenderer.flipX = true;
        }
    }
}
