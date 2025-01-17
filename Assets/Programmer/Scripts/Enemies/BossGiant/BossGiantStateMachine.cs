using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BossGiantStateMachine : StateManager<BossGiantStateMachine.BossGiantStates>, IBoss, IEnemy
{
    private BossGiantStateMachine Context;

    [Header("Main Variables")]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private CapsuleCollider2D _bodyColl;
    [SerializeField] private BoxCollider2D _feetColl;
    
    [Header("Health Variables")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] private int _damageFlashTime;



    #region Movement Checks
    [Header("Movement Checks")]
    [SerializeField] private LayerMask _groundDetectionLayer;
    [SerializeField] private float _groundDetectionRayLength;

    private RaycastHit2D _groundHit;
    private bool _isGrounded;
    #endregion

    [Header("Idle State Variables")]
    [SerializeField] private float _idleTimer = 2.5f;

    [Header("Attack State Variables")]
    [Header("Ground Attack")]
    [Header("If distance to player bigger then value, boss will do Jump")]
    [SerializeField] private float _attackDistance = 5f;
    [SerializeField] private float _attackAnimationLength;
    [SerializeField] private float _attackAnimationSpeed;
    [Header("Jump Attack")]
    [SerializeField] private AnimationCurve _jumpVerticalAnimationCurve;
    [SerializeField] private AnimationCurve _jumpHorizontalAnimationCurve;



    #region Main Vars
    private EventBus _eventBus;
    private IEnemyHealth _healthManager;
    private Rigidbody2D _rigidBody2D;
    private BossGiantAnimator _animator;
    private SpriteRenderer _spriteRenderer;
    private VFXManager _vFXManager;
    private PauseManager _pauseManager;
    private SoundsManager _soundsManager;
    private SoundsController _soundsController;
    #endregion

    #region Properties
    public VFXManager VFXManager { get { return _vFXManager; } }
    public IEnemyHealth HealthManager { get { return _healthManager; } }
    public BossGiantAnimator Animator { get { return _animator; } }
    public Rigidbody2D Rigidbody { get { return _rigidBody2D; } }
    public PauseManager PauseManager { get { return _pauseManager; } }
    public SoundsController SoundsController { get { return _soundsController; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float Health { get { return _health; } set { _health = value; } }
    public Transform Player { get { return _playerPosition; } }
    public LayerMask PlayerLayer { get { return _playerLayer; } }
    public float AttackDistance { get { return _attackDistance; } }
    public AnimationCurve JumpVerticalAnimationCurve { get { return _jumpVerticalAnimationCurve; } }
    public AnimationCurve JumpHorizontalAnimationCurve { get { return _jumpHorizontalAnimationCurve; } }
    public LayerMask GroundDetectionLayer { get { return _groundDetectionLayer; } }
    public float GroundDetectionRayLength { get { return _groundDetectionRayLength; } }


    public RaycastHit2D GroundHit { get { return _groundHit; } }
    public bool IsGrounded { get { return _isGrounded; } }
    public float IdleTimer { get { return _idleTimer; } }



    public float AttackAnimationLength { get { return _attackAnimationLength; } }
    public float AttackAnimationSpeed { get { return _attackAnimationSpeed; } }
    #endregion

    

    public enum BossGiantStates
    {
        SpawnState,
        Idle,
        Jump,
        Landing,
        Attack,
        Die
    }

    public void Init(EventBusManager eventBusManager, Transform player)
    {
        _eventBus = eventBusManager.EventBus;
        _vFXManager = eventBusManager.GetComponent<VFXManager>();
        _pauseManager = eventBusManager.GetComponent<PauseManager>();
        _soundsManager = eventBusManager.GetComponent<SoundsManager>();
        _soundsController = GetComponentInChildren<SoundsController>();
        _soundsController.SoundsManager = _soundsManager;
        _playerPosition = player;
    }

    private void Awake()
    {
        Context = this;
        _healthManager = new BossGiantHealth(Context);

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<BossGiantAnimator>();

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();


        States = new Dictionary<BossGiantStates, BaseState<BossGiantStates>>
        {
            { BossGiantStates.SpawnState, new BossGiantSpawnState(BossGiantStates.SpawnState, Context) },
            { BossGiantStates.Idle, new BossGiantIdle(BossGiantStates.Idle, Context) },
            { BossGiantStates.Jump, new BossGiantJump(BossGiantStates.Jump, Context) },
            { BossGiantStates.Landing, new BossGiantLanding(BossGiantStates.Landing, Context) },
            { BossGiantStates.Attack, new BossGiantAttack(BossGiantStates.Attack, Context) },
            { BossGiantStates.Die, new BossGiantDie(BossGiantStates.Die, Context) },
        };

        CurrentState = States[BossGiantStates.SpawnState];
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

    public async UniTask ChangeMaterial()
    {
        if (OnPause) return;

        Material currentMaterial = _spriteRenderer.material;
        _spriteRenderer.material = _vFXManager.EnemyDamageMaterial();

        await UniTask.Delay(_damageFlashTime); // Задержка в 1 секунду

        _spriteRenderer.material = currentMaterial;
    }

#if UNITY_EDITOR
    [Header("If Unity Editor\nDebug Gizmos")]
    [SerializeField] private bool _debugRays;
#endif

    public void IsGroundedCheck()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.center.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, _groundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, _groundDetectionRayLength, _groundDetectionLayer);

        if(_groundHit.collider != null)
            _isGrounded = true;
        else
            _isGrounded = false;

#if UNITY_EDITOR
        if (_debugRays)
        {
            Color rayColor;
            
            if (_isGrounded)
                rayColor = Color.green;
            else 
                rayColor = Color.red;

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * _groundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * _groundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - _groundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);
#endif
        }
    }
}
