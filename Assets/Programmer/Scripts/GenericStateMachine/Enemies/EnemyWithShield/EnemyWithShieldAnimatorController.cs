using UnityEngine;

public class EnemyWithShieldAnimatorController : MonoBehaviour
{
    private Animator animator;

    private readonly string Idle = "MobWithShield_Idle";
    private readonly string Walk = "MobWithShield_Walk";

    private void Awake() => animator = GetComponent<Animator>();

    public void DoIdle() => animator.Play(Idle);
    public void DoWalk() => animator.Play(Walk);
}
