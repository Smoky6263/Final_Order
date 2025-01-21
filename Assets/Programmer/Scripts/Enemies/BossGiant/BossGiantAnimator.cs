using UnityEngine;

public class BossGiantAnimator : MonoBehaviour
{
    private Animator _animator;
    public int JumpHash {get; private set;}
    public int IdleHash { get; private set;}
    public int AttackHash { get; private set;}
    public int InAirHash { get; private set;}
    public int LandingHash { get; private set;}

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        JumpHash = Animator.StringToHash("Jump");
        IdleHash = Animator.StringToHash("Idle");
        AttackHash = Animator.StringToHash("Attack");
        InAirHash = Animator.StringToHash("InAir");
        LandingHash = Animator.StringToHash("Landing");
    }

    public void PlayAnimation(int AnimationHash) => _animator.Play(AnimationHash);
    public void SetPlay() => _animator.speed = 1f;
    public void SetPause() => _animator.speed = 0f;

}
