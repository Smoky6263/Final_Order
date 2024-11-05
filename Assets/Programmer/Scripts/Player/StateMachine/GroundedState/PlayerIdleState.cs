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
        
        //IF ON STAIRS
        if (Context.IsGrounded && Context.OnStairs && Context.MovementInput.y != 0)
            SwitchState(Factory.OnStairs());
    }

    public override void EnterState()
    {
        //------------------------------------------------------
        //DO IDLE ANIMATION
        //------------------------------------------------------

        Context.MovementVelocity = new Vector2(0f, Context.MovementVelocity.y);
        Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.MovementVelocity.y);
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
