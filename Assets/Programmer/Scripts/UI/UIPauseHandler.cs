using UnityEngine;

public class UIPauseHandler : MonoBehaviour, IPauseHandler
{
    private PauseManager _PauseManager;
    private Animator _animator;

    public void Init(PauseManager pauseManager)
    {
        
    }
    private void Start()
    {
        _PauseManager = GameManager.Instance.GetComponent<PauseManager>();
        _PauseManager.Register(this);

        _animator = GetComponent<Animator>();
    }

    public void SetPlay()
    {
        _animator.speed = 1f;
    }

    public void SetPause()
    {
        _animator.speed = 0f;
    }

    public void OnDestroy() => Unregister();
    public void Unregister() => _PauseManager.Unregister(this);


}
