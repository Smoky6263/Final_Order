using UnityEngine;
using VContainer;

public class KeyPauseHandler : MonoBehaviour, IPauseHandler
{
    [Inject] private PauseManager _pauseManager;

    private ParticleSystem _particleSystem;
    private Animator _animator;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponentInChildren<Animator>();
        _pauseManager.Register(this);
    }

    public void SetPause()
    {
        _particleSystem.Pause();
        _animator.speed = 0f;
    }

    public void SetPlay()
    {
        _particleSystem.Play();
        _animator.speed = 1f;
    }
    public void Unregister()
    {
        _pauseManager.Unregister(this);
    }

    public void OnDestroy() => Unregister();


}
