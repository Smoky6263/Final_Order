using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    
    private float _elapsedTime; // Прошедшее время
    private float _speed;

    public override void CheckSwitchStates()
    {
        if (_elapsedTime <= 0 && (Context.IsGrounded || Context.OnStairs) && Context.BumpedHead == true)
        {
            Context.BodyColl.enabled = false;
            Context.BumpedHead = true;
            SwitchState(Factory.Crouch());
        }


        if (_elapsedTime <= 0 && Context.MovementInput.x == 0 && Context.MovementInput.y >= 0 && (Context.IsGrounded || Context.OnStairs) && Context.BumpedHead == false) 
        {
            Context.BodyColl.enabled = true;
            Context.BumpedHead = false;
            SwitchState(Factory.Idle());
        }

        if (_elapsedTime <= 0 && Context.MovementInput.x != 0 && (Context.IsGrounded || Context.OnStairs) && Context.BumpedHead == false)
        {
            Context.BodyColl.enabled = true;
            Context.BumpedHead = false;
            SwitchState(Factory.Run());
        }

        if (_elapsedTime <= 0 && Context.MovementInput.x == 0 && Context.MovementInput.y < 0 && (Context.IsGrounded || Context.OnStairs))
        {
            Context.BodyColl.enabled = true;
            Context.BumpedHead = false;
            SwitchState(Factory.Crouch());
        }

        if (_elapsedTime <= 0 && Context.IsGrounded == false && Context.OnStairs == false)
        {
            Context.BodyColl.enabled = true;
            Context.BumpedHead = false;
            SwitchState(Factory.Fall());
        }
    }

    public override void EnterState()
    {
        Context.BodyColl.enabled = false;
        Context.AnimatorController.DoRoll();
        Context.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_Dust, Context.transform.position));
        _elapsedTime = Context.RollDuration;
        _speed = Context.IsFacingRight ? Context.MoveStats.MaxRunSpeed : -Context.MoveStats.MaxRunSpeed;
    }

    public override void ExitState()
    {
        Context.MovementVelocity = new Vector2(0f, Context.MovementVelocity.y);
        Context.RollInput = false;
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        DoRolll();
        BumpHead();
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

    private void DoRolll()
    {
        _elapsedTime -= Time.fixedDeltaTime;

        // Ограничиваем прошедшее время длительностью анимации
        if (_elapsedTime < 0) 
            _elapsedTime = 0; // Обеспечиваем, что значение не выйдет за пределы продолжительности

        // Вычисляем новое значение с использованием функции EaseOutCubic
        float rollSpeed = EaseOutCubic(_elapsedTime, Context.MoveStats.RollDuration, _speed);

        Context.RigidBody.velocity = new Vector2(rollSpeed, Context.MovementVelocity.y);

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

    public override void PlayerOnAttackAnimationComplete()
    {
        
    }
}
