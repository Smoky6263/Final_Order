using UnityEngine;

public class EnemyWithShieldWalk : BaseState<EnemyWithShieldFSM.EnemyWithShieldStates>
{
    public EnemyWithShieldWalk(EnemyWithShieldFSM.EnemyWithShieldStates key, EnemyWithShieldFSM context) : base(key, context)
    {
        Context = context;
    }
    
    private EnemyWithShieldFSM Context;
    private enum OnRoute
    {
        A,
        B
    }
    
    private OnRoute _onRoute;

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        Walk();
    }

    public override EnemyWithShieldFSM.EnemyWithShieldStates GetNextState()
    {
        return EnemyWithShieldFSM.EnemyWithShieldStates.Walk;
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.GetComponent<IMobWithShieldTrigger>() != null)
        {
            if (_onRoute == OnRoute.A)
            {
                Context.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                _onRoute = _onRoute = OnRoute.B;

            }

            else if (_onRoute == OnRoute.B)
            {
                Context.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _onRoute = OnRoute.A;

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
        Context.AnimatorController.DoWalk();
        
        switch (_onRoute)
        {
            case OnRoute.A:
                Context.RigidBody2D.velocity = new Vector2(-Context.PatrollingSpeed, Context.RigidBody2D.velocity.y);
                break;

            case OnRoute.B:
                Context.RigidBody2D.velocity = new Vector2(Context.PatrollingSpeed, Context.RigidBody2D.velocity.y);
                break;

            default:
                Debug.Log("Нет цели, к которой идти");
                break;
        }

    }
}