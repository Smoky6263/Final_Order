using UnityEngine;

public class EnemyWithShieldIdle : BaseState<EnemyWithShieldFSM.EnemyWithShieldStates>
{
    public EnemyWithShieldIdle(EnemyWithShieldFSM.EnemyWithShieldStates key, object context) : base(key, context)
    {
        Context = (EnemyWithShieldFSM)context;
    }

    private EnemyWithShieldFSM Context;
    private float _time;


    public override void EnterState()
    {
        _time = 0f;
    }

    public override void ExitState()
    {
        _time = 0f;
    }

    public override void FixedUpdateState()
    {
        Context.AnimatorController.DoIdle();
        _time += Time.fixedDeltaTime;
    }

    public override EnemyWithShieldFSM.EnemyWithShieldStates GetNextState()
    {
        if (_time > Context.IdleTime) return EnemyWithShieldFSM.EnemyWithShieldStates.Walk;

        return EnemyWithShieldFSM.EnemyWithShieldStates.Idle;
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
