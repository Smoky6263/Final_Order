using UnityEngine;

public class EnemyWithShieldAnimatorController : MonoBehaviour
{
    private Animator animator;

    private readonly string _idle = "Idle";
    private readonly string _walk = "Walk";

    private void Awake() => animator = GetComponent<Animator>();

    public void DoIdle() => animator.SetTrigger(_idle);
    public void DoWalk() => animator.SetTrigger(_walk);
}
