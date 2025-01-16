using UnityEngine;

public class BossGiantAttack : BaseState<BossGiantStateMachine.BossGiantStates>
{
    public BossGiantAttack(BossGiantStateMachine.BossGiantStates key, BossGiantStateMachine context) : base(key, context)
    {
        Data= context;

        _attackLength = Data.AttackAnimationLength / Data.AttackAnimationSpeed;
    }

    private BossGiantStateMachine Data;

    private float _attackLength;
    private float _timer;


    public override void EnterState()
    {
        Data.Animator.PlayAnimation(Data.Animator.AttackHash);
    }

    public override void ExitState()
    {
        Data.Animator.PlayAnimation(Data.Animator.IdleHash);
        _timer = 0f;
    }

    public override void FixedUpdateState()
    {
        _timer += Time.fixedDeltaTime;
    }

    public override BossGiantStateMachine.BossGiantStates GetNextState()
    {
        if (_timer >= _attackLength)
            return BossGiantStateMachine.BossGiantStates.Idle;

        return BossGiantStateMachine.BossGiantStates.Attack;
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
}
