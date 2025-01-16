using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    protected float _accelerationSpeed { get { return Context.MoveStats.GroundAcceleration; } }
    protected float _decelerationSpeed { get { return Context.MoveStats.GroundDeceleration; } }

    public override void CheckSwitchStates()
    {
        //IF IDLE
        if ((Context.IsGrounded || Context.OnStairs) && Context.RigidBody.velocity.x == 0f)
            SwitchState(Factory.Idle());

        //IF PRESSED "CROUCH BUTTON" AND RUN BUTTONS RELEASED
        if(Context.IsGrounded && Context.OnStairs == false && Context.MovementInput.x == 0f && Context.MovementInput.y < 0)
            SwitchState(Factory.Crouch());

        //DO ROLL WHEN PRESS RUN AND ROLL BUTTON
        if (Context.IsGrounded && Context.RollInput == true)
            SwitchState(Factory.Roll());
    }

    public override void EnterState()
    {
        InitializeSubState();

        Context.BodyColl.enabled = true;

        // Если анимация АТАКИ у торса еще не закончена, тогда только ноги должны проиграть LegsRun анимацию
        // Иначе целиком проигрываем Idle анимацию на двух слоях
        int currentTorsoStateHash = Context.AnimatorController.GetCurrentAnimationStateHash(Context.AnimatorController.TorsoAnimator);

        if (currentTorsoStateHash == Context.AnimatorController.TorsoAttackHash)
            Context.AnimatorController.LegsAnimator.Play(Context.AnimatorController.LegsRun, 0, 0f);
        else
            Context.AnimatorController.OnRun();

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
        CheckAtack();
        CheckSwitchStates();
    }

    private void Move()
    {
        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(Context.MovementInput.x, 0f) * Context.MoveStats.MaxRunSpeed;

        TurnCheck(Context.MovementInput);

        Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, targetVelocity, _accelerationSpeed * Time.fixedDeltaTime);
        Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);
        
        //IF MoveInput == 0 => SwitchState to BRING TO IDLE
        if (Context.MovementInput.x == 0f)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, _decelerationSpeed * Time.fixedDeltaTime);
            Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);

            if ((Context.IsFacingRight == false && Context.RigidBody.velocity.x > -0.2f) || (Context.IsFacingRight == true && Context.RigidBody.velocity.x < 0.2f))
                Context.RigidBody.velocity = new Vector2(0f, Context.RigidBody.velocity.y);
        }
    }
    private void CheckAtack()
    {
        if (Context.AttackInput == true)
        {
            Context.AnimatorController.DoAttack();
            Context.AttackInput = false;
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
            Context.transform.rotation = Quaternion.Euler(0f,0f,0f);
            Context.WeaponController.BoxOffset = new Vector3(Context.WeaponController.DamageBox_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
            Context.VFXManager.SpawnDustParticles(Context.transform.position);
        }
        else
        {
            Context.IsFacingRight = false;
            Context.transform.rotation = Quaternion.Euler(0f,180f,0f);
            Context.WeaponController.BoxOffset = new Vector3(-Context.WeaponController.DamageBox_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
            Context.VFXManager.SpawnDustParticles(Context.transform.position);
        }
    }
    public override void PlayerOnAttackAnimationComplete()
    {
        Context.AnimatorController.OnRun();
    }
}
