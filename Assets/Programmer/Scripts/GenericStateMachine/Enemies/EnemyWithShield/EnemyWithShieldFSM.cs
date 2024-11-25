using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(EnemyPauseHandler))]

public class EnemyWithShieldFSM : StateManager<EnemyWithShieldFSM.EnemyWithShieldStates>, IEnemy
{
    [SerializeField] private EventBusManager _eventBus;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private VFXManager _vFXManager;
    [SerializeField] private EnemyWithShieldAnimatorController _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private EnemyWithShieldFSM Context;
    private EnemyHealth _healthManager;
    private Rigidbody2D _rigidBody2D;

    [Header("Параметры здоровья")]
    [SerializeField, Range(0f, 500f)] private float _maxHealth = 100f;
    [SerializeField, Range(0f, 500f)] private float _health = 0f;

    [Header("Время, которое моб проводит в состоянии Idle\n(например когда игрок убежал от него)")]
    [SerializeField, Range(0f, 10f)] private float _idleTime = 3f;

    [Header("Параметры скорости моба:\n- скорость при патрулировании\n- скорость при преследовании игрока")]
    [SerializeField, Range(0f, 10f)] private float _patrollingSpeed = 2f;
    [SerializeField, Range(0f, 10f)] private float _palyerFollowSpeed = 4f;

    [Header("Параметры урона в мсек")]
    [SerializeField, Range(0f, 2000f)] private int _damageFlashTime = 200;


    public Rigidbody2D RigidBody2D { get { return _rigidBody2D; } }
    public VFXManager VFXManager { get { return _vFXManager; } }
    public PauseManager PauseManager { get { return _pauseManager; } }
    public EnemyHealth HealthManager { get { return _healthManager; } }
    public EnemyWithShieldAnimatorController AnimatorController { get { return _animator; } }
    public float IdleTime { get { return _idleTime; } }
    public float PatrollingSpeed { get { return _patrollingSpeed; } }
    public float PlayerFollowSpeed { get { return _palyerFollowSpeed; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float Health { get { return _health; } set { _health = value; } }


    public enum EnemyWithShieldStates
    {
        Idle,
        Walk,
        FollowPlayer,
        Die
    }

    private void Awake()
    {
        Context = this;
        _healthManager = new EnemyHealth(Context);
        _rigidBody2D = GetComponent<Rigidbody2D>();

        States = new Dictionary<EnemyWithShieldStates, BaseState<EnemyWithShieldStates>>
        {
            { EnemyWithShieldStates.Idle, new EnemyWithShieldIdle(EnemyWithShieldStates.Idle, Context) },
            { EnemyWithShieldStates.Walk, new EnemyWithShieldWalk(EnemyWithShieldStates.Walk, Context) },
            { EnemyWithShieldStates.FollowPlayer, new EnemyWithShieldFollowPlayer(EnemyWithShieldStates.FollowPlayer, Context) },
            { EnemyWithShieldStates.Die, new EnemyWithShieldDie(EnemyWithShieldStates.Die, Context) },
        };

        CurrentState = States[EnemyWithShieldStates.Idle];
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition() 
    {
        return transform.position;
    }

    private CancellationTokenSource _onDestroyToken;
    public async UniTask ChangeMaterial()
    {
        if (OnPause) return;

        _onDestroyToken = new CancellationTokenSource();

        Material currentMaterial = _spriteRenderer.material;
        _spriteRenderer.material = _vFXManager.GetDamageMaterial();

        await UniTask.Delay(_damageFlashTime, cancellationToken: _onDestroyToken.Token); // Задержка в 1 секунду

        _spriteRenderer.material = currentMaterial;
    }

    private void OnDestroy()
    {
        if (_onDestroyToken != null)
        {
            _onDestroyToken?.Cancel();
            _onDestroyToken.Dispose();
        }
    }
}
