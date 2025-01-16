using UnityEngine;

internal class EnemyWithWeaponFollowPlayer : BaseState<EnemyWithWeaponStateMachine.EnemyWithWeaponStates>
{
    public EnemyWithWeaponFollowPlayer(EnemyWithWeaponStateMachine.EnemyWithWeaponStates key, EnemyWithWeaponStateMachine context) : base(key, context)
    {
        Context = context;

    }

    private EnemyWithWeaponStateMachine Context;
    
    public override void EnterState()
    {
        Context.AnimatorController.DoWalk();
    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        FollowPlayer();
        CheckPlayer();
    }

    public override EnemyWithWeaponStateMachine.EnemyWithWeaponStates GetNextState()
    {
        if (Context.PlayerDetected == false)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fight", 0);
            return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Idle;
        }

        if (Context.OnAttack == true)
            return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Attack;

        return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.FollowPlayer;
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

    protected virtual void FollowPlayer()
    {
        float distance = Vector2.Distance(Context.transform.position, Context.PlayerPosition);

        bool playerOnRightSide = Context.transform.position.x < Context.PlayerPosition.x;
        Context.transform.rotation = playerOnRightSide ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        Context.Weapon.WeaponFacingRight(playerOnRightSide);

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fight", 1);

        if (distance < Context.AttackDistance) 
        {
            Context.OnAttack = true;
            return;
        }

        if(playerOnRightSide) Context.MovementVelocity = new Vector2(Context.PlayerFollowSpeed, Context.RigidBody2D.velocity.y);
        else Context.MovementVelocity = new Vector2(-Context.PlayerFollowSpeed, Context.RigidBody2D.velocity.y);

        Context.RigidBody2D.velocity = Vector2.Lerp(Context.RigidBody2D.velocity, Context.MovementVelocity, 0.1f);
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