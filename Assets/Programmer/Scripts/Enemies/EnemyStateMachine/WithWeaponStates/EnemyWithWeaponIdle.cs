using UnityEngine;

public class EnemyWithWeaponIdle : BaseState<EnemyWithWeaponStateMachine.EnemyWithWeaponStates>
{
    public EnemyWithWeaponIdle(EnemyWithWeaponStateMachine.EnemyWithWeaponStates key, EnemyWithWeaponStateMachine context) : base(key, context)
    {
        Context = context;
    }

    private EnemyWithWeaponStateMachine Context;
    private float _time;


    public override void EnterState()
    {
        _time = 0f;
        Context.AnimatorController.DoIdle();
    }

    public override void ExitState()
    {
        _time = 0f;
    }

    public override void FixedUpdateState()
    {
        Vector2 movementVelocity = new Vector2(0f, Context.RigidBody2D.velocity.y);
        Context.RigidBody2D.velocity = Vector2.Lerp(Context.RigidBody2D.velocity, movementVelocity, 0.1f);
        
        _time += Time.fixedDeltaTime;
    }

    public override EnemyWithWeaponStateMachine.EnemyWithWeaponStates GetNextState()
    {
        if (_time > Context.IdleTime) return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Walk;

        return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Idle;
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
