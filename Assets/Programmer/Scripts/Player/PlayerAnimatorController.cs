using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;

    //ANIMATOR VARS
    public string Crouch { get; private set; } = "Crouch";
    public string Idle { get; private set; } = "Idle";
    public string Run { get; private set; } = "Run";

    private void Awake() => _animator = GetComponent<Animator>();

    public void OnCrouch(bool value) => _animator.SetBool(Crouch, value);
    public void OnIdle(bool value) => _animator.SetBool(Idle, value);
    public void OnRun(bool value) => _animator.SetBool(Run, value);
}
