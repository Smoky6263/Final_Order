using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _torsoAnimator, _legsAimator;

    public Animator TorsoAnimator { get { return _torsoAnimator; } }
    public Animator LegsAnimator { get { return _legsAimator; } }

    //ANIMATOR TORSO VARS
    public readonly string TorsoAttack = "TorsoAttack";
    public readonly string TorsoCrouch = "TorsoCrouch";
    public readonly string TorsoIdle = "TorsoIdle";
    public readonly string TorsoRun = "TorsoRun";
    public readonly string TorsoRoll = "TorsoRoll";
    public readonly string TorsoJump = "TorsoJump";
    public readonly string TorsoDeath = "TorsoDeath";


    //ANIMATOR LEGS VARS
    public readonly string LegsAttack = "LegsAttack";
    public readonly string LegsCrouch = "LegsCrouch";
    public readonly string LegsIdle = "LegsIdle";
    public readonly string LegsRun = "LegsRun";
    public readonly string LegsRoll = "LegsRoll";
    public readonly string LegsJump = "LegsJump";
    public readonly string LegsDeath = "LegsDeath";


    public void OnCrouch()
    {
        _torsoAnimator.Play(TorsoCrouch);
        _legsAimator.Play(LegsCrouch);
    }
    public void OnIdle()
    {
        _torsoAnimator.Play(TorsoIdle, 0 ,0f);
        _legsAimator.Play(LegsIdle, 0, 0f);
    }
    public void OnRun()
    {
        _torsoAnimator.Play(TorsoRun);
        _legsAimator.Play(LegsRun);
    }
    public void DoAttack()
    {
        _torsoAnimator.Play(TorsoAttack);
    }

    public void DoRoll()
    {
        _torsoAnimator.Play(TorsoRoll);
        _legsAimator.Play(LegsRoll);
    }

    public void DoJump()
    {
        _torsoAnimator.Play(TorsoJump);
        _legsAimator.Play(LegsJump);
    }

    public void OnDeath()
    {
        _torsoAnimator.Play(TorsoDeath);
        _legsAimator.Play(LegsDeath);
    }

    public void ResetCurrentAnimationTime(Animator animator, string animationName)
    {
        animator.Play(animationName, 0, 0f);
    }
}
