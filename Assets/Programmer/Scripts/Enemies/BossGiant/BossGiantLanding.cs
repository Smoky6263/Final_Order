using UnityEngine;

public class BossGiantLanding : BaseState<BossGiantStateMachine.BossGiantStates>
{
    public BossGiantLanding(BossGiantStateMachine.BossGiantStates key, BossGiantStateMachine context) : base(key, context)
    {
        Data = context;
    }

    private BossGiantStateMachine Data;

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override BossGiantStateMachine.BossGiantStates GetNextState()
    {
        return BossGiantStateMachine.BossGiantStates.Landing;
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
