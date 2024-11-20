using UnityEngine;

public class EnemyWithShieldDie : BaseState<EnemyWithShieldFSM.EnemyWithShieldStates>
{
    public EnemyWithShieldDie(EnemyWithShieldFSM.EnemyWithShieldStates key, object context) : base(key, context)
    {
        Context = (EnemyWithShieldFSM)context;
    }

    private EnemyWithShieldFSM Context;

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
        return EnemyWithShieldFSM.EnemyWithShieldStates.Die;
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