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

        AnimatorStateInfo stateInfo = Context.AnimatorController.TorsoAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(Context.AnimatorController.TorsoAttack))
        {
            Animator legs = Context.AnimatorController.LegsAnimator;
            string LegsIdle = Context.AnimatorController.LegsIdle;

            Context.AnimatorController.ResetCurrentAnimationTime(legs, LegsIdle);
        }
        else
            Context.AnimatorController.OnIdle();

    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {

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

    public override void OnPlayerOnAttackAnimationComplete()
    {
        Animator legs = Context.AnimatorController.LegsAnimator;
        Animator torso = Context.AnimatorController.TorsoAnimator;

        string legsIdle = Context.AnimatorController.LegsIdle;
        string torsoIdle = Context.AnimatorController.TorsoIdle;

        Context.AnimatorController.ResetCurrentAnimationTime(legs, legsIdle);
        Context.AnimatorController.ResetCurrentAnimationTime(torso, torsoIdle);
    }
}
