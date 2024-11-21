using UnityEngine;
public class PlayerJumpFromStairsState : PlayerBaseState
{
    public PlayerJumpFromStairsState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    float _elapsedTime;

    public override void CheckSwitchStates()
    {
        //IF LANDED
        if ((Context.IsJumping || Context.IsFalling) && Context.IsGrounded && Context.VerticalVelocity <= 0f)
            SwitchState(Factory.Grounded());

        //IF ON STAIRS
        if ((Context.IsJumping || Context.IsFalling) && Context.OnStairs && Context.MovementInput.y != 0f)
            SwitchState(Factory.Grounded());

        //IF PASS END OF DUREATION
        if (Context.VerticalVelocity < 0.1f || Context.BumpedHead == true)
            SwitchState(Factory.Fall());

    }

    public override void EnterState()
    {
        InitializeSubState();
        
        Context.AnimatorController.DoJump();
        Context.RollInput = false;

        Context.VerticalVelocity = Context.MoveStats.InitialJumpFromStairsVelocity;
        _elapsedTime = Context.MoveStats.JumpfAfterStairsDuration;
    }

    public override void ExitState()
    {
        Context.BumpedHead = false;

    }

    public override void InitializeSubState()
    {
        //IF PLAYER FALLING AND RUN
        SetSubState(Factory.FallingRun());
    }

    public override void UpdateState()
    {
        BumpHead();
        DoJump();
        TurnCheck(Context.MovementInput);
        CountTimers();
        CheckSwitchStates();
    }


    // Функция ease-out cubic, которая будет изменять значение на основе прошедшего времени
    float EaseOutCubic(float t, float T, float startValue)
    {
        // Нормализуем t (процесс времени) относительно длительности T
        float normalizedTime = t / T;

        // Функция ease-out cubic: f(t) = startValue * (1 - (1 - t)^3)
        return startValue * (1 - Mathf.Pow(1 - normalizedTime, 3));
    }

    private void DoJump()
    {
        _elapsedTime -= Time.fixedDeltaTime;

        // Ограничиваем прошедшее время длительностью анимации
        if (_elapsedTime < 0)
            _elapsedTime = 0; // Обеспечиваем, что значение не выйдет за пределы продолжительности

        // Вычисляем новое значение с использованием функции EaseOutCubic
        Context.VerticalVelocity = EaseOutCubic(_elapsedTime, Context.MoveStats.RollDuration, Context.VerticalVelocity);
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);
    }

    private void BumpHead()
    {
        Vector2 boxCastOrigin = new Vector2(Context.FeetColl.bounds.center.x, Context.BodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(Context.FeetColl.bounds.size.x * Context.MoveStats.HeadWidth, Context.MoveStats.HeadDetectionRayLength);

        Context.HeadHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, Context.MoveStats.HeadDetectionRayLength, Context.MoveStats.HeadBumpSurfaceLayer);

        if (Context.HeadHit.collider != null)
        {
            Context.IsFastFalling = true;
            Context.BumpedHead = true;
        }

        else { Context.BumpedHead = false; }
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
            Context.VFXManager.SpawnDustParticles(Context.transform.position);
        }
        else
        {
            Context.IsFacingRight = false;
            Context.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            Context.WeaponController.BoxOffset = new Vector3(-Context.WeaponController.Box_X_value, Context.WeaponController.BoxOffset.y, Context.WeaponController.BoxOffset.z);
            Context.VFXManager.SpawnDustParticles(Context.transform.position);
        }
    }

    #region Timers
    private void CountTimers()
    {
        if(Context.JumpInput)
            Context.JumpBufferTimer -= Time.deltaTime;
    }
    #endregion
}