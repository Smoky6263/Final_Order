using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currenrContext, PlayerStateFactory playerStateFactory) : base(currenrContext, playerStateFactory)
    {
        InitializeSubState();
    }

    public override void CheckSwitchStatesState()
    {
        //IF IDLE
        if(Context.IsGrounded && Context.MovementInput.x == 0)
            SwitchState(Factory.Idle());

        //IF JUMP PRESSED
        if(Context.IsGrounded && Context.JumpButtonPressed)
            SwitchState(Factory.Jump());
    }

    public override void EnterState()
    {
        TurnCheck(Context.MovementInput);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Move();
        CheckSwitchStatesState();
    }

    private void Move()
    {

        
        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(Context.MovementInput.x, 0f) * Context.MoveStats.MaxRunSpeed;

        TurnCheck(Context.MovementInput);

        Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, targetVelocity, Context.GroundAcceleration * Time.fixedDeltaTime);
        Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);

        //IF MoveInput == 0 => SwitchState to BRING TO IDLE
        //if (Context.MovementVelocity != Vector2.zero)
        //{
        //    Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, Context.GroundDeceleration * Time.fixedDeltaTime);
        //    Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);
        //}
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
