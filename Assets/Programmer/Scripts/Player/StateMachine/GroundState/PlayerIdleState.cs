using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        InitializeSubState();
    }

    public override void CheckSwitchStates()
    {
        //IF PLAYER PRESS MOVE INPUT
        if(Context.IsGrounded && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());
    }

    public override void EnterState()
    {
        Debug.Log("Enter IdleState");
        Context.MovementVelocity = new Vector2(0f, Context.MovementVelocity.y);
        Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.MovementVelocity.y);
    }

    public override void ExitState()
    {
        Debug.Log("Exit IdleState");
    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
