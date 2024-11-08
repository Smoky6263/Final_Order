using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }
    
    private float _elapsedTime; // ��������� �����

    public override void CheckSwitchStates()
    {

        if (_elapsedTime <= 0 && Context.RigidBody.velocity.x == 0 && Context.IsGrounded)
            SwitchState(Factory.Idle());

        if(_elapsedTime >= Context.MoveStats.RollDuration &&  Context.RigidBody.velocity.x != 0 && Context.IsGrounded)
            SwitchState(Factory.Run());

        if (_elapsedTime >= Context.MoveStats.RollDuration &&  Context.IsGrounded == false)
            SwitchState(Factory.Fall());
    }

    public override void EnterState()
    {
        Context.AnimatorController.OnCrouch(true);
        _elapsedTime = Context.RollDuration;
    }

    public override void ExitState()
    {
        Context.MovementVelocity = new Vector2(0f, Context.MovementVelocity.y);
        Context.AnimatorController.OnCrouch(false);
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
        float rollSpeed = EaseOutCubic(_elapsedTime, Context.MoveStats.RollDuration, Context.MovementVelocity.x);

        Debug.Log("Value: " + rollSpeed);

        Context.RigidBody.velocity = new Vector2(rollSpeed, Context.MovementVelocity.y);

    }
}
