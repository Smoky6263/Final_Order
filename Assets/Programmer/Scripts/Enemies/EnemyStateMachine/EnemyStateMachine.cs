using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(EnemyPauseHandler))]
[RequireComponent(typeof(EnemyDamageTrigger))]

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyStates>, IStandartEnemy, IEnemy
{
    [SerializeField] private EventBusManager _eventBus;
    [SerializeField] private EnemyAnimatorController _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _playerLayer;

    #region MyTarget
    [SerializeField] private EnemyPatrolTrigger _pointA;
    [SerializeField] private EnemyPatrolTrigger _pointB;

    public EnemyPatrolTrigger PointA { get {return _pointA; } }
    public EnemyPatrolTrigger PointB { get { return _pointB; } }
    #endregion

    private EnemyStateMachine Context;
    private PauseManager _pauseManager;
    private VFXManager _vFXManager;
    private SoundsManager _soundsManager;
    private SoundsController _soundsController;
    private IEnemyHealth _healthManager;
    private Rigidbody2D _rigidBody2D;

    protected bool _playerDetected;

    [Header("ѕараметры здоровь€")]
    [SerializeField, Range(0f, 500f)] private float _maxHealth = 100f;
    [SerializeField, Range(0f, 500f)] private float _health = 0f;

    [Header("¬рем€, которое моб проводит в состо€нии Idle\n(например когда игрок убежал от него)")]
    [SerializeField, Range(0f, 10f)] private float _idleTime = 3f;

    [Header("ѕараметры скорости моба:\n- скорость при патрулировании\n- скорость при преследовании игрока")]
    [SerializeField, Range(0f, 10f)] private float _patrollingSpeed = 2f;
    [SerializeField, Range(0f, 10f)] private float _palyerFollowSpeed = 4f;

    [Header("ѕараметры урона в мсек")]
    [SerializeField, Range(0f, 2000f)] private int _damageFlashTime = 200;

    [Header("размер зоны обнаружени€ игрока")]
    [SerializeField] private Vector2 _playerDetectionArea;
    [SerializeField] private Vector3 _playerDetectionAreaOffset;

    [Header("размер зоны преследовани€ игрока")]
    [SerializeField] private Vector2 _playerFollowArea;
    [SerializeField] private Vector3 _playerFollowAreaOffset;


    public Rigidbody2D RigidBody2D { get { return _rigidBody2D; } }
    public VFXManager VFXManager { get { return _vFXManager; } }
    public PauseManager PauseManager { get { return _pauseManager; } }
    public SoundsManager SoundsManager { get { return _soundsManager; } }
    public SoundsController SoundsController { get { return _soundsController; } }
    public IEnemyHealth HealthManager { get { return _healthManager; } }
    public EnemyAnimatorController AnimatorController { get { return _animator; } }
    public LayerMask PlayerLayer { get { return _playerLayer; } }
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

    private void Awake()
    {
        Context = this;

        _healthManager = new EnemyHealth(Context);

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _vFXManager = _eventBus.GetComponent<VFXManager>();
        _pauseManager = _eventBus.GetComponent<PauseManager>();
        _soundsManager = _eventBus.GetComponent<SoundsManager>();

        _soundsController = GetComponentInChildren<SoundsController>();


        States = new Dictionary<EnemyStates, BaseState<EnemyStates>>
        {
            { EnemyStates.Idle, new EnemyIdle(EnemyStates.Idle, Context) },
            { EnemyStates.Walk, new EnemyPatrolling(EnemyStates.Walk, Context) },
            { EnemyStates.FollowPlayer, new EnemyFollowPlayer(EnemyStates.FollowPlayer, Context) },
            { EnemyStates.Die, new EnemyDie(EnemyStates.Die, Context) },
        };

        CurrentState = States[EnemyStates.Idle];
    }

    public void Die()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fight", 0);
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
        _spriteRenderer.material = _vFXManager.EnemyDamageMaterial();

        await UniTask.Delay(_damageFlashTime, cancellationToken: _onDestroyToken.Token); // «адержка в 1 секунду

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

#if UNITY_EDITOR
    [Header("IF IN UNITY EDITOR")]
    [SerializeField] private bool _debugPlayerDetectionArea;
    [SerializeField] private bool _debugPlayerFolowArea;
    [SerializeField] private Color _detectionColor, _followColor;

    private void OnDrawGizmos()
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
