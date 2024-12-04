using UnityEngine;
public class EnemyPauseHandler : MonoBehaviour, IPauseHandler
{
    private Rigidbody2D _rigidbody2D;
    private EnemyDamageTrigger _damageTrigger;
    private Animator _animator;
    private PauseManager _pauseManager;
    private IEnemy _enemy;
    
    public void Init(PauseManager pauseManager)
    {
        
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _damageTrigger = GetComponent<EnemyDamageTrigger>();

        _animator = GetComponentInChildren<Animator>();

        _enemy = GetComponent<IEnemy>();
        _pauseManager = _enemy.PauseManager;
        _pauseManager.Register(this);
    }

    public void SetPlay()
    {
        _damageTrigger.enabled = true;
        _animator.speed = 1.0f;
        _rigidbody2D.simulated = true;
        _enemy.OnPause = false;
    }

    public void SetPause()
    {
        _damageTrigger.enabled = false;
        _animator.speed = 0f;
        _rigidbody2D.simulated = false;
        _enemy.OnPause = true;
    }

    public void OnDestroy() => Unregister();

    public void Unregister() => _pauseManager.Unregister(this);

}