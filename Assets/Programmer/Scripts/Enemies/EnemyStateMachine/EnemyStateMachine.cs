using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(EnemyPauseHandler))]
[RequireComponent(typeof(EnemyDamageTrigger))]

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyStates>, IStandartEnemy, IEnemy
{
    protected EnemyStateMachine Context;

    [Inject] protected GameManager _gameManager;
    [SerializeField] protected EnemyAnimatorController _animator;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected LayerMask _playerLayer;

    #region MyTarget
    [SerializeField] private EnemyPatrolTrigger _pointA;
    [SerializeField] private EnemyPatrolTrigger _pointB;

    public EnemyPatrolTrigger PointA { get {return _pointA; } }
    public EnemyPatrolTrigger PointB { get { return _pointB; } }
    #endregion

    protected PauseManager _pauseManager;
    protected EventBus _eventBus;
    protected VFXManager _vFXManager;
    protected SoundsManager _soundsManager;
    protected SoundsController _soundsController;
    protected IEnemyHealth _healthManager;
    protected Rigidbody2D _rigidBody2D;

    protected bool _playerDetected;

    [Header("ѕараметры здоровь€")]
    [SerializeField, Range(0f, 500f)] protected float _maxHealth = 100f;
    [SerializeField, Range(0f, 500f)] protected float _health = 0f;

    [Header("¬рем€, которое моб проводит в состо€нии Idle\n(например когда игрок убежал от него)")]
    [SerializeField, Range(0f, 10f)] protected float _idleTime = 3f;

    [Header("ѕараметры скорости моба:\n- скорость при патрулировании\n- скорость при преследовании игрока")]
    [SerializeField, Range(0f, 10f)] protected float _patrollingSpeed = 2f;
    [SerializeField, Range(0f, 10f)] protected float _palyerFollowSpeed = 4f;

    [Header("ѕараметры урона в мсек")]
    [SerializeField, Range(0f, 2000f)] protected int _damageFlashTime = 200;

    [Header("размер зоны обнаружени€ игрока")]
    [SerializeField] protected Vector2 _playerDetectionArea;
    [SerializeField] protected Vector3 _playerDetectionAreaOffset;

    [Header("размер зоны преследовани€ игрока")]
    [SerializeField] protected Vector2 _playerFollowArea;
    [SerializeField] protected Vector3 _playerFollowAreaOffset;


    public Rigidbody2D RigidBody2D { get { return _rigidBody2D; } }
    public VFXManager VFXManager { get { return _vFXManager; } }
    public PauseManager PauseManager { get { return _pauseManager; } }
    public SoundsManager SoundsManager { get { return _soundsManager; } }
    public SoundsController SoundsController { get { return _soundsController; } }
    public IEnemyHealth HealthManager { get { return _healthManager; } }
    public EnemyAnimatorController AnimatorController { get { return _animator; } }
    public LayerMask PlayerLayer { get { return _playerLayer; } }
    public EventBus EventBus { get { return _eventBus; } }
    public Vector2 PlayerDetectionArea { get { return _playerDetectionArea; } }
    public Vector3 PlayerDetectionAreaOffset { get { return _playerDetectionAreaOffset; } }

    public Vector2 PlayerFollowArea { get { return _playerFollowArea; } }
    public Vector3 PlayerFollowAreaOffset { get { return _playerFollowAreaOffset; } }

    public bool PlayerDetected { get { return _playerDetected; } set { _playerDetected = value; } }

    public float IdleTime { get { return _idleTime; } }
    public float PatrollingSpeed { get { return _patrollingSpeed; } }
    public float PlayerFollowSpeed { get { return _palyerFollowSpeed; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float Health { get { return _health; } set { _health = value; } }


    public enum EnemyStates
    {
        Idle,
        Walk,
        FollowPlayer,
        Die
    }

    protected void Awake()
    {
        Context = this;
        _eventBus = _gameManager.EventBus;

        _healthManager = new EnemyHealth(Context);

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _vFXManager = _gameManager.GetComponent<VFXManager>();
        _pauseManager = _gameManager.GetComponent<PauseManager>();
        _soundsManager = _gameManager.GetComponent<SoundsManager>();
        _soundsController = GetComponentInChildren<SoundsController>();
        _soundsController.SoundsManager = _soundsManager;


        States = new Dictionary<EnemyStates, BaseState<EnemyStates>>
        {
            { EnemyStates.Idle, new EnemyIdle(EnemyStates.Idle, Context) },
            { EnemyStates.Walk, new EnemyPatrolling(EnemyStates.Walk, Context) },
            { EnemyStates.FollowPlayer, new EnemyFollowPlayer(EnemyStates.FollowPlayer, Context) },
            { EnemyStates.Die, new EnemyDie(EnemyStates.Die, Context) },
        };

        CurrentState = States[EnemyStates.Idle];
    }

    public virtual void Die()
    {
        Destroy(transform.parent.gameObject);
    }

    public Vector3 GetPosition() 
    {
        return transform.position;
    }

    protected CancellationTokenSource _onDestroyToken;
    public async UniTask ChangeMaterial()
    {
        if (OnPause) return;

        _onDestroyToken = new CancellationTokenSource();

        Material currentMaterial = _spriteRenderer.material;
        _spriteRenderer.material = _vFXManager.EnemyDamageMaterial();

        await UniTask.Delay(_damageFlashTime, cancellationToken: _onDestroyToken.Token); // «адержка в 1 секунду

        _spriteRenderer.material = currentMaterial;
    }

    protected void OnDestroy()
    {
        if (_onDestroyToken != null)
        {
            _onDestroyToken?.Cancel();
            _onDestroyToken.Dispose();
        }
    }

#if UNITY_EDITOR
    [Header("IF IN UNITY EDITOR")]
    [SerializeField] private bool _debugPlayerDetectionArea;
    [SerializeField] private bool _debugPlayerFolowArea;
    [SerializeField] private Color _detectionColor, _followColor;

    protected void OnDrawGizmos()
    {
        if (_debugPlayerDetectionArea)
        {
            Gizmos.color = _detectionColor;
            Gizmos.DrawWireCube(transform.position + _playerDetectionAreaOffset, _playerDetectionArea);
        }

        if(_debugPlayerFolowArea)
        {
            Gizmos.color = _followColor;
            Gizmos.DrawWireCube(transform.position + _playerFollowAreaOffset, _playerFollowArea);
        }
    }

#endif

}
