using UnityEngine;

public class VFXScript : MonoBehaviour, IPauseHandler
{
    [SerializeField] private float _lifeTime = 2f;

    private PauseManager _pauseManager;
    private ParticleSystem _particleSystem;
    private bool _onPause;
    public void Init(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
        _pauseManager.Register(this);
    }

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (_onPause) return;

        _lifeTime -= Time.fixedDeltaTime;

        if(_lifeTime < 0 )
            Destroy(gameObject);
    }
    public void SetPause()
    {
        _particleSystem.Pause();
        _onPause = true;
    }

    public void SetPlay()
    {
        _particleSystem.Play();
        _onPause = false;
    }
    public void Unregister()
    {
        _pauseManager.Unregister(this);
    }

    public void OnDestroy() => Unregister();

    
}
