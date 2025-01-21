using UnityEngine;

public class BossGiantPauseHandler : MonoBehaviour, IPauseHandler
{
    private BossGiantStateMachine _bossGiantStateMachine;
    private Rigidbody2D _rigidbody2D;
    private BossGiantAnimator _bossGiantossAnimator;
    private PauseManager _pauseManager;

    public void Init(PauseManager pauseManager)
    {
        
    }

    private void Start()
    {
        _bossGiantStateMachine = GetComponent<BossGiantStateMachine>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _bossGiantossAnimator = GetComponent<BossGiantAnimator>();

        _pauseManager = _bossGiantStateMachine.PauseManager;
        _pauseManager.Register(this);
    }

    public void OnDestroy()
    {
        Unregister();
    }

    public void SetPause()
    {
        _rigidbody2D.simulated = false;
        _bossGiantossAnimator.SetPause();

        if (_bossGiantStateMachine.OnCutScene) return;
        _bossGiantStateMachine.OnPause = true;
    }

    public void SetPlay()
    {
        _bossGiantossAnimator.SetPlay();
        _rigidbody2D.simulated = true;

        if (_bossGiantStateMachine.OnCutScene) return;
        _bossGiantStateMachine.OnPause = false;
    }

    public void Unregister()
    {
        _pauseManager.Unregister(this);
    }


}
