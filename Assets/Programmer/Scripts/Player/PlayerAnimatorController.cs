using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _torsoAnimator, _legsAimator;

    //ANIMATOR TORSO VARS
    public string TorsoAttack { get; private set; } = "TorsoAttack";
    public string TorsoCrouch { get; private set; } = "TorsoCrouch";
    public string TorsoIdle { get; private set; } = "TorsoIdle";
    public string TorsoRun { get; private set; } = "TorsoRun";

    //ANIMATOR LEGS VARS
    public string LegsAttack { get; private set; } = "LegsAttack";
    public string LegsCrouch { get; private set; } = "LegsCrouch";
    public string LegsIdle { get; private set; } = "LegsIdle";
    public string LegsRun { get; private set; } = "LegsRun";


    public void OnCrouch()
    {
        _torsoAnimator.SetTrigger(TorsoCrouch);
        _legsAimator.SetTrigger(LegsCrouch);
    }
    public void OnIdle()
    {
        _torsoAnimator.SetBool(TorsoIdle, true);
        _legsAimator.SetTrigger(LegsIdle);
    }
    public void OnRun()
    {
        _torsoAnimator.SetBool(TorsoRun, true);
        _legsAimator.SetTrigger(LegsRun);
    }
    public void DoAttack(bool isIdle)
    {
        _torsoAnimator.SetTrigger(TorsoAttack);

        if(isIdle )
            _legsAimator.SetTrigger(LegsAttack);
    }

    public void ResetTorso()
    {
        _torsoAnimator.SetBool(TorsoRun, false);
        _torsoAnimator.SetBool(TorsoIdle, false);
    }
}
