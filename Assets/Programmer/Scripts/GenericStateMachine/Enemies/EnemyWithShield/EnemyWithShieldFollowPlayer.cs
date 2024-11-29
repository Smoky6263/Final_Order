using UnityEngine;

internal class EnemyWithShieldFollowPlayer : BaseState<EnemyWithShieldFSM.EnemyWithShieldStates>
{
    public EnemyWithShieldFollowPlayer(EnemyWithShieldFSM.EnemyWithShieldStates key, StateManager<EnemyWithShieldFSM.EnemyWithShieldStates> context) : base(key, context) { }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState()
    {
    }

    public override EnemyWithShieldFSM.EnemyWithShieldStates GetNextState()
    {
        return EnemyWithShieldFSM.EnemyWithShieldStates.FollowPlayer;
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