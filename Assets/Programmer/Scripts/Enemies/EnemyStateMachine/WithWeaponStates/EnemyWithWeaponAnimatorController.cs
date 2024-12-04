using UnityEngine;

public class EnemyWithWeaponAnimatorController : MonoBehaviour
{
    private Animator animator;

    public readonly string Idle = "Idle";
    public readonly string Walk = "Walk";
    public readonly string Attack = "Attack";


    private void Awake() => animator = GetComponentInChildren<Animator>();

    public void DoIdle() => animator.Play(Idle);
    public void DoWalk() => animator.Play(Walk);
    public void DoAttack() => animator.Play(Attack);
}
