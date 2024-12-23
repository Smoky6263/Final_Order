using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;

    public readonly string Idle = "Idle";
    public readonly string Walk = "Walk";


    private void Awake() => animator = GetComponent<Animator>();

    public void DoIdle() => animator.Play(Idle);
    public void DoWalk() => animator.Play(Walk);
}
