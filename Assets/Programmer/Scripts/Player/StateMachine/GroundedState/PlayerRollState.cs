using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    
    private float _elapsedTime; // ��������� �����
    private float _speed;

    public override void CheckSwitchStates()
    {

        if (_elapsedTime <= 0 && Context.MovementInput.x == 0 && Context.MovementInput.y >= 0 && (Context.IsGrounded || Context.OnStairs))
            SwitchState(Factory.Idle());

        if(_elapsedTime <= 0 &&  Context.MovementInput.x != 0 && (Context.IsGrounded || Context.OnStairs))
            SwitchState(Factory.Run());

        if (_elapsedTime <= 0 && Context.MovementInput.x == 0 && Context.MovementInput.y < 0 && (Context.IsGrounded || Context.OnStairs))
            SwitchState(Factory.Crouch());

        if (_elapsedTime <= 0 &&  Context.IsGrounded == false && Context.OnStairs == false)
            SwitchState(Factory.Fall());
    }

    public override void EnterState()
    {
        Context.BodyColl.enabled = false;
        Context.AnimatorController.OnCrouch();
        Context.VFXManager.SpawnDustParticles(Context.transform.position);
        _elapsedTime = Context.RollDuration;
        _speed = Context.IsFacingRight ? Context.MoveStats.MaxRunSpeed : -Context.MoveStats.MaxRunSpeed;
    }

    public override void ExitState()
    {
        Context.BodyColl.enabled = true;
        Context.MovementVelocity = new Vector2(0f, Context.MovementVelocity.y);
        Context.RollInput = false;
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        DoRolll();
        CheckSwitchStates();
    }


    // ������� ease-out cubic, ������� ����� �������� �������� �� ������ ���������� �������
    float EaseOutCubic(float t, float T, float startValue)
    {
        // ����������� t (������� �������) ������������ ������������ T
        float normalizedTime = t / T;

        // ������� ease-out cubic: f(t) = startValue * (1 - (1 - t)^3)
        return startValue * (1 - Mathf.Pow(1 - normalizedTime, 3));
    }

    private void DoRolll()
    {
        _elapsedTime -= Time.fixedDeltaTime;

        // ������������ ��������� ����� ������������� ��������
        if (_elapsedTime < 0) 
            _elapsedTime = 0; // ������������, ��� �������� �� ������ �� ������� �����������������

        // ��������� ����� �������� � �������������� ������� EaseOutCubic
        float rollSpeed = EaseOutCubic(_elapsedTime, Context.MoveStats.RollDuration, _speed);

        Debug.Log("Value: " + rollSpeed);

        Context.RigidBody.velocity = new Vector2(rollSpeed, Context.MovementVelocity.y);

    }
}
