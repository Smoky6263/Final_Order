using System;
using UnityEngine;

internal class PlayerOnDeathState : PlayerBaseState
{
    public PlayerOnDeathState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void EnterState()
    {
        Context.CharacterController.enabled = false;
        Context.ResetInputs();
        Context.AnimatorController.OnDeath();
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }


    public override void UpdateState()
    {
        if(Context.MovementVelocity.x != 0)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, Context.MoveStats.GroundDeceleration * Time.fixedDeltaTime);
            Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);
        }
    }
}
