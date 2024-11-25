using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _torsoAnimator, _legsAimator;

    private EventBus _eventBus;

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

    public int TorsoAttackHash { get; private set; }
    public int TorsoCrouchHash { get; private set; }
    public int TorsoIdleHash { get; private set; }
    public int TorsoRunHash { get; private set; }
    public int TorsoRollHash { get; private set; }
    public int TorsoJumpHash { get; private set; }
    public int TorsoDeathHash { get; private set; }


    private void Awake()
    {
        _eventBus = GetComponent<PlayerStateMachine>().EventBus;

        TorsoAttackHash = Animator.StringToHash(TorsoAttack);
        TorsoCrouchHash = Animator.StringToHash(TorsoCrouch);
        TorsoIdleHash = Animator.StringToHash(TorsoIdle);
        TorsoRunHash = Animator.StringToHash(TorsoRun);
        TorsoRollHash = Animator.StringToHash(TorsoRoll);
        TorsoJumpHash = Animator.StringToHash(TorsoJump);
        TorsoDeathHash = Animator.StringToHash(TorsoDeath);
    }

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

    /// <summary>
    /// ¬ качестве параметра передайте Animator, у которого хотите узнать Hash текущей проигрываемой анимации.
    /// </summary>
    /// <param name="animator"></param>
    /// <returns>
    /// ¬озвращает int со значением хэш кода анимации, котора€ проигрываетс€ в данный момент.
    /// </returns>
    public int GetCurrentAnimationStateHash(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
    }

    public void SetPlay()
    {
        TorsoAnimator.speed = 1f;
        LegsAnimator.speed = 1f;
    }
    public void SetPause()
    {
        TorsoAnimator.speed = 0f;
        LegsAnimator.speed = 0f;
    }
}
