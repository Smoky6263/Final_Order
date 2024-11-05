using System;
using UnityEngine;

public class PlayerOnStairsState : PlayerBaseState
{
    public PlayerOnStairsState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        InitializeSubState();
    }

    protected float _accelerationSpeed { get { return Context.MoveStats.AirAcceleration; } }
    protected float _decelerationSpeed { get { return Context.MoveStats.AirDeceleration; } }

    public override void CheckSwitchStates()
    {
        //IF PLAYER PRESS MOVE INPUT AND EXIT FROM STAIRS
        if (Context.IsGrounded && Context.OnStairs == false && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());

        //IF PRESS JUMP
        if(Context.JumpButtonPressed == true)
            SwitchState(Factory.Jump());
    }

    public override void EnterState()
    {
        //------------------------------------------------------
        //DO STAIRS ANIMATION
        //------------------------------------------------------
        
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        Move();
        CheckSwitchStates();
    }

    private void Move()
    {
        Vector2 targetVelocity = Vector2.zero;
        targetVelocity = new Vector2(Context.MovementInput.x, Context.MovementInput.y) * Context.MoveStats.MaxRunSpeed;

        Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, targetVelocity, _accelerationSpeed * Time.fixedDeltaTime);
        Context.RigidBody.velocity = Context.MovementVelocity;

        //IF MoveInput == 0 => SwitchState to BRING TO IDLE
        if (Context.MovementInput == Vector2.zero)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, _decelerationSpeed * Time.fixedDeltaTime);
            Context.RigidBody.velocity = Context.MovementVelocity;
        }
    }
}
