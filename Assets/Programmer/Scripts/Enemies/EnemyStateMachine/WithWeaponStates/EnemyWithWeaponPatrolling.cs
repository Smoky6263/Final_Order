using UnityEngine;

public class EnemyWithWeaponPatrolling : BaseState<EnemyWithWeaponStateMachine.EnemyWithWeaponStates>
{
    public EnemyWithWeaponPatrolling(EnemyWithWeaponStateMachine.EnemyWithWeaponStates key, EnemyWithWeaponStateMachine context) : base(key, context)
    {
        Context = context;

        _pointA = Context.PointA;
        _PointB = Context.PointB;

        _patrolTarget = _pointA.transform.position;
    }
    
    private EnemyWithWeaponStateMachine Context;

    private EnemyPatrolTrigger _pointA;
    private EnemyPatrolTrigger _PointB;

    private Vector3 _patrolTarget;


    public override void EnterState()
    {
        Context.AnimatorController.DoWalk();
    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        Walk();
        CheckPlayer();
    }

    public override EnemyWithWeaponStateMachine.EnemyWithWeaponStates GetNextState()
    {
        if (Context.PlayerDetected)
            return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.FollowPlayer;

        return EnemyWithWeaponStateMachine.EnemyWithWeaponStates.Walk;
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.GetComponent<IEnemyTrigger>() != null)
        {
            //IF ENTER A TRIGGER
            if (collision.GetComponent<IEnemyTrigger>().UniqueID == _pointA.UniqueID)
            {
                _patrolTarget = _PointB.transform.position;
            }
            //IF ENTER B TRIGGER
            else if (collision.GetComponent<IEnemyTrigger>().UniqueID == _PointB.UniqueID)
            {
                _patrolTarget = _pointA.transform.position;
            }
        }
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

    private void Walk()
    {
        bool pointOnRightSide = Context.transform.position.x < _patrolTarget.x;
        Context.transform.rotation = pointOnRightSide ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        Context.Weapon.WeaponFacingRight(pointOnRightSide);

        if (pointOnRightSide) Context.MovementVelocity = new Vector2(Context.PlayerFollowSpeed, Context.RigidBody2D.velocity.y);
        else Context.MovementVelocity = new Vector2(-Context.PlayerFollowSpeed, Context.RigidBody2D.velocity.y);

        Context.RigidBody2D.velocity = Vector2.Lerp(Context.RigidBody2D.velocity, Context.MovementVelocity, 0.1f);
    }

    private void CheckPlayer()
    {
        Collider2D collider = Physics2D.OverlapBox(Context.GetPosition() + Context.PlayerDetectionAreaOffset, Context.PlayerDetectionArea, 0f, Context.PlayerLayer);

        if(collider != null) 
            Context.PlayerDetected = true;

        else
            Context.PlayerDetected = false;
    }
}