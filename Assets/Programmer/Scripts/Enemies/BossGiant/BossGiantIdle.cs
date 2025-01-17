using UnityEngine;

public class BossGiantIdle : BaseState<BossGiantStateMachine.BossGiantStates>
{
    public BossGiantIdle(BossGiantStateMachine.BossGiantStates key, BossGiantStateMachine context) : base(key, context)
    {
        Data = context;
    }

    private BossGiantStateMachine Data;

    private float _idleTimer;

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        Rotate();
        _idleTimer = 0f;
    }

    public override void FixedUpdateState()
    {
        Timer();
    }


    public override BossGiantStateMachine.BossGiantStates GetNextState()
    {
        if (_idleTimer >= Data.IdleTimer && Vector3.Distance(Data.transform.position, Data.Player.position) < Data.AttackDistance)
            return BossGiantStateMachine.BossGiantStates.Attack;
        else if(_idleTimer >= Data.IdleTimer && Vector3.Distance(Data.transform.position, Data.Player.position) > Data.AttackDistance)
            return BossGiantStateMachine.BossGiantStates.Jump;

        return BossGiantStateMachine.BossGiantStates.Idle;
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
    private void Timer()
    {
        _idleTimer += Time.fixedDeltaTime;
    }
    private void Rotate()
    {
        if (Data.transform.position.x < Data.Player.position.x)
            Data.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            Data.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
