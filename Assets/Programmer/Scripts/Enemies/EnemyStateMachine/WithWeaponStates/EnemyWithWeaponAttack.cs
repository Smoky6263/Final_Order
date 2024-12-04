using UnityEngine;

class EnemyWithWeaponAttack : BaseState<EnemyWithWeaponStateMachine.EnemyWithWeaponStates>
{
    public EnemyWithWeaponAttack(EnemyWithWeaponStateMachine.EnemyWithWeaponStates key, EnemyWithWeaponStateMachine context) : base(key, context)
    {
        Context = context;
    }

    private EnemyWithWeaponStateMachine Context;
    
    public override void EnterState()
    {
        Context.AnimatorController.DoAttack();
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {
        Vector2 movementVelocity = new Vector2(0f, Context.RigidBody2D.velocity.y);
        Context.RigidBody2D.velocity = Vector2.Lerp(Context.RigidBody2D.velocity, movementVelocity, 0.1f);

        bool playerOnRightSide = Context.transform.position.x < Context.PlayerPosition.x;
        Context.transform.rotation = playerOnRightSide ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        Context.Weapon.WeaponFacingRight(playerOnRightSide);

        CheckPlayer();
    }

    public override EnemyWithWeaponStateMachine.EnemyWithWeaponStates GetNextState()
    {
        float distance = Vector2.Distance(Context.transform.position, Context.PlayerPosition);

        if (Context.OnAttack == false && distance > Context.AttackDistance * 2f)
            return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.FollowPlayer;

        if(Context.OnAttack == false && Context.PlayerDetected == false)
            return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Idle;

        return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Attack;
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

    private void CheckPlayer()
    {
        Collider2D collider = Physics2D.OverlapBox(Context.GetPosition() + Context.PlayerFollowAreaOffset, Context.PlayerFollowArea, 0f, Context.PlayerLayer);

        if (collider != null)
        {
            Context.PlayerPosition = collider.transform.position;
            Context.PlayerDetected = true;
        }
        else
        {
            Context.PlayerDetected = false;
        }
    }
}
