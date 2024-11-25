using UnityEngine;
public class PlayerFallingRunState : PlayerBaseState
{
    public PlayerFallingRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    protected float _accelerationSpeed { get { return Context.MoveStats.AirAcceleration; } }
    protected float _decelerationSpeed { get { return Context.MoveStats.AirDeceleration; } }

    public override void CheckSwitchStates()
    {

    }

    public override void EnterState()
    {
        InitializeSubState();


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

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        Move();
        CheckAtack();
        TurnCheck(Context.MovementInput);
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
        if (Context.MovementInput == Vector2.zero)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, _decelerationSpeed * Time.fixedDeltaTime);
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
            Context.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(Context.WeaponController.Box_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
        }
        else
        {
            Context.IsFacingRight = false;
            Context.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(-Context.WeaponController.Box_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
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

    public override void PlayerOnAttackAnimationComplete()
    {
        Context.AnimatorController.DoJump();
    }
}
