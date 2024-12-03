using UnityEngine;

class PlayerOnDamageState : PlayerBaseState
{
    public PlayerOnDamageState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    private float _elapsedTime;
    private Vector2 _applyForce;

    private Material _material;

    public override void CheckSwitchStates()
    {
        if (_elapsedTime <= 0)
            SwitchState(Factory.Fall());
    }

    public override void EnterState()
    {
        _elapsedTime = Context.DamageDelayTime;
        _applyForce = Context.PlayerHealth.ApplyForce;

        _material = Context.TorsoSprite.material;

        Context.TorsoSprite.material = Context.VFXManager.PlayerDamageMaterial();
        Context.LegsSprite.material = Context.VFXManager.PlayerDamageMaterial();

        Context.AnimatorController.DoJump();
    }

    public override void ExitState()
    {
        Context.TorsoSprite.material = _material;
        Context.LegsSprite.material = _material;

        Context.PlayerHealth.TurnOffDamageDelay();
    }

    public override void InitializeSubState()
    {
        
    }


    public override void UpdateState()
    {
        ApplyDamage();
        CheckSwitchStates();
    }

    private void ApplyDamage()
    {
        _elapsedTime -= Time.fixedDeltaTime;

        // Ограничиваем прошедшее время длительностью анимации
        if (_elapsedTime < 0)
            _elapsedTime = 0; // Обеспечиваем, что значение не выйдет за пределы продолжительности

        // Вычисляем новое значение с использованием функции EaseOutCubic
        Context.MovementVelocity = EaseOutCubic(_elapsedTime, Context.DamageDelayTime, _applyForce);
        Context.RigidBody.velocity = Context.MovementVelocity;
    }

    // Функция ease-out cubic, которая будет изменять значение на основе прошедшего времени
    Vector2 EaseOutCubic(float t, float T, Vector2 startValue)
    {
        // Нормализуем t (процесс времени) относительно длительности T
        float normalizedTime = t / T;

        startValue.x *= (1 - Mathf.Pow(1 - normalizedTime, 3));
        startValue.y *= (1 - Mathf.Pow(1 - normalizedTime, 3));

        // Функция ease-out cubic: f(t) = startValue * (1 - (1 - t)^3)
        return startValue;
    }

    public override void PlayerOnAttackAnimationComplete() { }
}
