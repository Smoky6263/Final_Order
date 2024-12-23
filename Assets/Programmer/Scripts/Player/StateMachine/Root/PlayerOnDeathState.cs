using UnityEngine;
using UnityEngine.SceneManagement;

internal class PlayerOnDeathState : PlayerBaseState
{
    public PlayerOnDeathState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    private float _time;

    public override void CheckSwitchStates()
    {
        
    }

    public override void EnterState()
    {
        Context.CharacterController.enabled = false;
        Context.ResetInputs();
        Context.AnimatorController.OnDeath();

        _time = 0f;
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }

    public override void PlayerOnAttackAnimationComplete()
    {
        
    }

    public override void UpdateState()
    {
        DoPhysics();

        _time += Time.deltaTime;

        if (_time > 3f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DoPhysics()
    {
        //CLAMP FALLS SPEED
        Context.VerticalVelocity = Mathf.Clamp(Context.VerticalVelocity, -Context.MoveStats.MaxFallSpeed, 50f);
        Context.RigidBody.velocity = new Vector2(Context.RigidBody.velocity.x, Context.VerticalVelocity);

        if (Context.MovementVelocity.x != 0)
        {
            Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, Context.MoveStats.GroundDeceleration * Time.fixedDeltaTime);
            Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);
        }
    }
}
