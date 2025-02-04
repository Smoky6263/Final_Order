using UnityEngine;

public class PlayerOnStairsState : PlayerBaseState
{
    public PlayerOnStairsState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    protected float _accelerationSpeed { get { return Context.MoveStats.AirAcceleration; } }
    protected float _decelerationSpeed { get { return Context.MoveStats.AirDeceleration; } }

    public override void CheckSwitchStates()
    {

        //IF ON TOP OF STAIRS
        if (Context.OnStairs == false && Context.MovementInput.y > 0f && Context.JumpInput == false)
            SwitchState(Factory.JumpFromStairs());
        
        //IF PLAYER PRESS MOVE INPUT AND EXIT FROM STAIRS
        if (Context.IsGrounded && Context.OnStairs == false)
            SwitchState(Factory.Grounded());

        //IF PRESS JUMP
        if(Context.JumpInput == true)
            SwitchState(Factory.Jump());


        //IF PLAYER FALL
        if (Context.OnStairs == false && Context.IsGrounded == false && Context.JumpInput == false && Context.CoyoteTimer <= 0)
            SwitchState(Factory.Fall());
    }

    public override void EnterState()
    {
        InitializeSubState();

        Context.AnimatorController.DoStairs();
        Context.BodyColl.enabled = true;

        if (Context.OnCrouch)
            Context.OnCrouch = false;
        
        Context.CoyoteTimer = 0f;
        Context.VerticalVelocity = Physics2D.gravity.y;
    }

    public override void ExitState()
    {
        Context.AttackInput = false;
        Context.AnimatorController.LegsAnimator.speed = 1f;
    }

    public override void InitializeSubState()
    {

    }

    public override void PlayerOnAttackAnimationComplete()
    {
        
    }

    public override void UpdateState()
    {
        Move();
        TurnCheck(Context.MovementInput);
        CheckSwitchStates();
    }

    private void Move()
    {
        Context.AnimatorController.OnStairs(Context.MovementInput.magnitude);

        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(Context.MovementInput.x, Context.MovementInput.y) * Context.MoveStats.MaxRunSpeed;

        Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, targetVelocity, _accelerationSpeed * Time.fixedDeltaTime);
        Context.RigidBody.velocity = Context.MovementVelocity;

        //IF MoveInput == 0 => SwitchState to BRING TO IDLE
        if (Context.MovementInput == Vector2.zero)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, _decelerationSpeed * Time.fixedDeltaTime);
            Context.RigidBody.velocity = Context.MovementVelocity;
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
            Context.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(Context.WeaponController.DamageBox_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
            Context.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_Dust, Context.transform.position));

            Context.EventBus.Invoke(new CinemachineCallTurnSignal());
        }
        else
        {
            Context.IsFacingRight = false;
            Context.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(-Context.WeaponController.DamageBox_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
            Context.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_Dust, Context.transform.position));

            Context.EventBus.Invoke(new CinemachineCallTurnSignal());
        }
    }
}
