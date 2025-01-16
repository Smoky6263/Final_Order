using UnityEngine;

public class EnemyWithWeaponDie : BaseState<EnemyWithWeaponStateMachine.EnemyWithWeaponStates>
{
    public EnemyWithWeaponDie(EnemyWithWeaponStateMachine.EnemyWithWeaponStates key, EnemyWithWeaponStateMachine context) : base(key, context)
    {
        Context = context;
    }

    private EnemyWithWeaponStateMachine Context;

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {
        
    }

    public override EnemyWithWeaponStateMachine.EnemyWithWeaponStates GetNextState()
    {
        return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Die;
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