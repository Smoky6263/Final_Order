using UnityEngine;

internal class EnemyFollowPlayer : BaseState<EnemyStateMachine.EnemyStates>
{
    public EnemyFollowPlayer(EnemyStateMachine.EnemyStates key, EnemyStateMachine context) : base(key, context)
    {
        Context = context;

    }

    private EnemyStateMachine Context;
    private Vector2 _playerPosition;
    Vector2 movementVelocity;

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

    public override EnemyStateMachine.EnemyStates GetNextState()
    {
        if (Context.PlayerDetected == false)
            return EnemyStateMachine.EnemyStates.Idle;

        return EnemyStateMachine.EnemyStates.FollowPlayer;
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
        bool playerOnRightSide = Context.transform.position.x < _playerPosition.x;
        Context.transform.rotation = playerOnRightSide ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);

        if(playerOnRightSide) movementVelocity = new Vector2(Context.PlayerFollowSpeed, Context.RigidBody2D.velocity.y);
        else movementVelocity = new Vector2(-Context.PlayerFollowSpeed, Context.RigidBody2D.velocity.y);

        Context.RigidBody2D.velocity = Vector2.Lerp(Context.RigidBody2D.velocity, movementVelocity, 0.1f);
    }

    private void CheckPlayer()
    {
        Collider2D collider = Physics2D.OverlapBox(Context.GetPosition() + Context.PlayerFollowAreaOffset, Context.PlayerFollowArea, 0f, Context.PlayerLayer);

        if (collider != null)
        {
            _playerPosition = collider.transform.position;
            Context.PlayerDetected = true;
        }
        else
        {
            Context.PlayerDetected = false;
        }
    }
}