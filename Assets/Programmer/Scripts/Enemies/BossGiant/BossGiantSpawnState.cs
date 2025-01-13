using System;
using UnityEngine;

public class BossGiantSpawnState : BaseState<BossGiantStateMachine.BossGiantStates>
{
    public BossGiantSpawnState(BossGiantStateMachine.BossGiantStates key, BossGiantStateMachine context) : base(key, context)
    {
        Data = context;
    }

    private BossGiantStateMachine Data;

    public override void EnterState()
    {
        Data.Animator.PlayAnimation(Data.Animator.InAirHash);
    }

    public override void ExitState()
    {
        Data.Animator.PlayAnimation(Data.Animator.LandingHash);
    }

    public override void FixedUpdateState()
    {

    }

    public override BossGiantStateMachine.BossGiantStates GetNextState()
    {
        if(Data.IsGrounded) return BossGiantStateMachine.BossGiantStates.Idle;

        return BossGiantStateMachine.BossGiantStates.SpawnState;
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
        Data.IsGroundedCheck();
    }
}
