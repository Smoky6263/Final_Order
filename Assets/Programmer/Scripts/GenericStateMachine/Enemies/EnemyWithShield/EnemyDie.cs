using UnityEngine;

public class EnemyDie : BaseState<EnemyStateMachine.EnemyWithShieldStates>
{
    public EnemyDie(EnemyStateMachine.EnemyWithShieldStates key, EnemyStateMachine context) : base(key, context)
    {
        Context = context;
    }

    private EnemyStateMachine Context;

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {
        
    }

    public override EnemyStateMachine.EnemyWithShieldStates GetNextState()
    {
        return EnemyStateMachine.EnemyWithShieldStates.Die;
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