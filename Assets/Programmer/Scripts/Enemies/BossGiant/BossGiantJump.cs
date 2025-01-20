using UnityEngine;

public class BossGiantJump : BaseState<BossGiantStateMachine.BossGiantStates>
{
    public BossGiantJump(BossGiantStateMachine.BossGiantStates key, BossGiantStateMachine context) : base(key, context)
    {
        Data = context;
    }

    private BossGiantStateMachine Data;
    private float _currentTime;
    private float _totalTime;
    private Vector3 _playerPosition;

    public override void EnterState()
    {
        Data.Animator.PlayAnimation(Data.Animator.JumpHash);
        _playerPosition = Data.Player.position;
        _totalTime = Data.JumpVerticalAnimationCurve.keys[Data.JumpVerticalAnimationCurve.keys.Length - 1].time;
        _currentTime = 0f;
        Rotate();
    }

    public override void ExitState()
    {
        Data.Animator.PlayAnimation(Data.Animator.LandingHash);
    }

    public override void FixedUpdateState()
    {
        DoJump();
    }

    public override BossGiantStateMachine.BossGiantStates GetNextState()
    {
        if(_currentTime >= _totalTime) return BossGiantStateMachine.BossGiantStates.Idle;

        return BossGiantStateMachine.BossGiantStates.Jump;
    }

    public override void OnTriggerEnter(Collider2D collision)
    {

    }

    public override void OnTriggerExit(Collider2D collision)
    {

    }

    public override void OnTriggerStay(Collider2D collision)
    {

    }

    public override void UpdateState()
    {

    }

    private void DoJump()
    {
        float t = _currentTime / _totalTime;
        float horizontalCurveValue = Data.JumpHorizontalAnimationCurve.Evaluate(t);

        float jumpHeight = Data.transform.position.y + Data.JumpVerticalAnimationCurve.Evaluate(_currentTime);
        float jumpLength = Mathf.Lerp(Data.transform.position.x, _playerPosition.x, horizontalCurveValue);

        Data.transform.position = new Vector3(jumpLength, jumpHeight, Data.transform.position.z);

        _currentTime += Time.fixedDeltaTime;
    }

    private void Rotate()
    {
        if (Data.transform.position.x < Data.Player.position.x)
        {
            Data.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            Data.IsFacingRight = true;
        }
        else 
        {
            Data.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Data.IsFacingRight = false;
        }
    }
}
