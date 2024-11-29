using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {

    }

    public override void CheckSwitchStates()
    {
        //IF PLAYER PRESS MOVE INPUT
        if((Context.IsGrounded || Context.OnStairs) && Context.MovementInput.x != 0f)
            SwitchState(Factory.Run());
        
        //IF PLAYER PRESS "CROUCH" BUTTON
        if(Context.IsGrounded && Context.OnStairs == false && Context.MovementInput.x == 0 && Context.MovementInput.y < 0)
            SwitchState(Factory.Crouch());

        //DO ROLL WHEN PRESS ROLL BUTTON
        if ((Context.IsGrounded || Context.OnStairs) && Context.MovementInput.x == 0f && Context.RollInput == true)
            SwitchState(Factory.Roll());
    }

    public override void EnterState()
    {
        InitializeSubState();

        Context.BodyColl.enabled = true;


        // Если анимация АТАКИ у торса еще не закончена, тогда только ноги должны проиграть LegsAtack анимацию
        // Иначе целиком проигрываем Idle анимацию на двух слоях
        int currentTorsoStateHash = Context.AnimatorController.GetCurrentAnimationStateHash(Context.AnimatorController.TorsoAnimator);


        if (currentTorsoStateHash == Context.AnimatorController.TorsoAttackHash)
        {
            Context.AnimatorController.LegsAnimator.Play(Context.AnimatorController.LegsAttack, 0, 0f);
        }
        else 
        {
            Context.AnimatorController.OnIdle();
        }

    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {

        Context.MovementVelocity = Vector2.Lerp(Context.MovementVelocity, Vector2.zero, Context.MoveStats.GroundDeceleration * Time.fixedDeltaTime);
        Context.RigidBody.velocity = new Vector2(Context.MovementVelocity.x, Context.RigidBody.velocity.y);

        CheckAtack();
        CheckSwitchStates();
    }

    private void CheckAtack()
    {
        if (Context.AttackInput == true)
        {
            Context.AnimatorController.DoAttack();
            Context.AnimatorController.LegsAnimator.Play(Context.AnimatorController.LegsAttack);
            Context.AttackInput = false;
        }
    }

    public override void PlayerOnAttackAnimationComplete()
    {
        Animator legs = Context.AnimatorController.LegsAnimator;
        Animator torso = Context.AnimatorController.TorsoAnimator;

        string legsIdle = Context.AnimatorController.LegsIdle;
        string torsoIdle = Context.AnimatorController.TorsoIdle;

        Context.AnimatorController.ResetCurrentAnimationTime(legs, legsIdle);
        Context.AnimatorController.ResetCurrentAnimationTime(torso, torsoIdle);
    }
}
