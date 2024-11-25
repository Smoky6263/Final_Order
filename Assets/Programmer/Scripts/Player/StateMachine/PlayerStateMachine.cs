using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimatorController))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(PlayerPauseHandler))]
public class PlayerStateMachine : MonoBehaviour, IControlable
{
    private EventBus _eventBus;
    private PlayerHealth _playerHealth;
    private CharacterController _characterController;
    private Rigidbody2D _rigidBody;

    private PlayerAnimatorController _animatorController;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    public bool OnPause { get; set; } = false;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    [SerializeField] private EventBusManager _gameManager;
    [SerializeField] private VFXManager _vfxManager;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private PlayerStats _moveStats;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _feetColl;
    #region Player Fields

    [Header("Health Variables")]
    [SerializeField, Range(0f, 100f)] public float _maxHealth;
    [SerializeField, Range(0f, 100f)] public float _health;
    [Header("После получения урона, игрок не может получить\nпока не пройдет мсек:")]
    [SerializeField, Range(0f, 10000f)] private int _damageDelayTime;

    [Header("Weapon Variables")]
    [SerializeField] private PlayerWeaponController _weaponController;
    #region Collision Fiekds
    private bool _onStairs;
    private GameObject _currentPTP; /* PTP = PassTroughPlatform*/
    #endregion

    #endregion

    #region Input Fields
    //player inputs fields
    private Vector2 _movementInput;
    private bool _jumpButtonInput;
    private bool _rollInput;
    private bool _attackInput;

    //movement vars
    private Vector2 _movementVelocity;
    private bool _isFacingRight;
    private bool _onCrouch;

    //collision check vars
    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool _bumpedHead;

    //jump vars
    public float VerticalVelocity { get; set; }
    private bool _isJumping;
    private bool _isFastFalling;
    private float _fastFallReleaseSpeed;
    private bool _isFalling;
    private float _fastFallTime;

    //apex vars
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    //jump buffer vars
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer;


    //coyote time
    private float _coyoteTimer;
    #endregion

    #region Player Properties
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }
    public EventBus EventBus { get { return _eventBus; } }
    public VFXManager VFXManager { get { return _vfxManager; } }
    public PauseManager PauseManager { get { return _pauseManager; } }
    public PlayerStats MoveStats { get { return _moveStats; } }
    public CharacterController CharacterController{ get { return _characterController; } }
    public PlayerAnimatorController AnimatorController { get { return _animatorController; } }
    public Collider2D BodyColl { get { return _bodyColl; } }
    public Collider2D FeetColl { get { return _feetColl; } }
    public Rigidbody2D RigidBody { get { return _rigidBody; } }
    public PlayerHealth PayerHealth { get { return _playerHealth; } }
    public PlayerWeaponController WeaponController { get { return _weaponController; } }
    public int DamageDelayTime { get { return _damageDelayTime; } }


    //player inputs
    public Vector2 MovementInput { get { return _movementInput; } }
    public bool JumpInput { get { return _jumpButtonInput; } set { _jumpButtonInput = value; } }
    public bool RollInput { get { return _rollInput; } set { _rollInput = value; } }
    public bool AttackInput { get { return _attackInput; } set { _attackInput = value; } }


    #region Ccollision check vars
    public RaycastHit2D GroundHit { get { return _groundHit; } set { _groundHit = value; } }
    public RaycastHit2D HeadHit { get { return _headHit; } set { _headHit = value; } }
    public GameObject CurrentPTP { get { return _currentPTP; } set { _currentPTP = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool BumpedHead { get { return _bumpedHead; } set { _bumpedHead = value; } }
    public bool OnStairs { get { return _onStairs; } set { _onStairs = value; } }

    #endregion

    #region Movement and Jump Properties

    //movement vars
    public Vector2 MovementVelocity { get { return _movementVelocity; } set { _movementVelocity = value; } }
    public bool IsFacingRight { get { return _isFacingRight; } set { _isFacingRight = value; } }
    public bool OnCrouch { get { return _onCrouch; } set { _onCrouch = value; } }
    public float RollDuration { get { return _moveStats.RollDuration; } }
    public float JumpfAfterStairsDuration { get { return _moveStats.JumpfAfterStairsDuration; } }

    //jump vars
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public bool IsFastFalling { get { return _isFastFalling; } set { _isFastFalling = value; } }
    public float FastFallReleaseSpeed { get { return _fastFallReleaseSpeed; } set { _fastFallReleaseSpeed = value; } }
    public bool IsFalling { get { return _isFalling; } set { _isFalling = value; } }
    public float FastFallTime { get { return _fastFallTime; } set { _fastFallTime = value; } }

    //apex vars
    public float ApexPoint { get { return _apexPoint; } set { _apexPoint = value; } }
    public float TimePastApexThreshold { get { return _timePastApexThreshold; } set { _timePastApexThreshold = value; } }
    public bool IsPastApexThreshold { get { return _isPastApexThreshold; } set { _isPastApexThreshold = value; } }

    //jump buffer vars
    public float JumpBufferTimer { get { return _jumpBufferTimer; } set { _jumpBufferTimer = value; } }
    public bool JumpReleasedDuringBuffer { get { return _jumpReleasedDuringBuffer; } set { _jumpReleasedDuringBuffer = value; } }


    //coyote time
    public float CoyoteTimer { get { return _coyoteTimer; } set { _coyoteTimer = value; } }
    #endregion



    #endregion

    private void Awake()
    {
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<PlayerOnDeathSignal>(OnDeath);
        _eventBus.Subscribe<PlayerAttackAnimationCompleteSignal>(OnPlayerAttackAnimationComplete);
        GetComponentInChildren<PlayerWeaponController>().Init(_eventBus);
        _characterController = GetComponent<CharacterController>();
        _playerHealth = new PlayerHealth(this);
        _rigidBody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<PlayerAnimatorController>();
        _isFacingRight = true;
    }

    private void Start()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Fall();
        _currentState.EnterState();
    }

    private void Update()
    {
        if (OnPause) return;

        IsGroundedCheck();
    }

    private void FixedUpdate()
    {
        if (OnPause) return;

        _currentState.UpdateStates();
    }

    #region PlayerInputs
    public void MoveInput(float x, float y) => _movementInput = new Vector2(x, y);
    public void JumpIsPressed()
    {
        _jumpButtonInput = true;
        _jumpBufferTimer = _moveStats.JumpBufferTime;
    }
    public void JumpIsReleased() => _jumpButtonInput = false;
    public void RollPressed()
    {
        if(IsGrounded == true)
            _rollInput = true;
    }

    public void AttackPressed()
    {
        if(_rollInput == false)
            _attackInput = true;
    }

    public void ResetInputs()
    {
        _movementInput = Vector2.zero;
        _jumpButtonInput = false;
        _rollInput = false;
        _attackInput = false;
    }
    #endregion

    #region Collision Checks
    private void IsGroundedCheck()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.center.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, _moveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, _moveStats.GroundDetectionRayLength, _moveStats.JumpSurfaceLayer);
        //_groundHit = Physics2D.OverlapBox()
        if (_groundHit.collider != null)
            _isGrounded = true;

        else { _isGrounded = false; }

        #region DebugVisualization

        if (_moveStats.DebugShowIsGroundedBox)
        {
            Color rayColor;
            if (_isGrounded)
                rayColor = Color.green;

            else { rayColor = Color.red; }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * _moveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * _moveStats.GroundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - _moveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);

            #endregion
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnPause) return;

        if (collision.tag == "Stairs")
            _onStairs = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (OnPause) return;

        if (collision.tag == "Stairs")
            _onStairs = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnPause) return;

        if (collision.gameObject.GetComponent<PlatformEffector2D>() != null)
            _currentPTP = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (OnPause) return;

        if (collision.gameObject.GetComponent<PlatformEffector2D>() != null)
            _currentPTP = null;
    }

    private void OnGUI()
    {
        GUIStyle textSTyle = new GUIStyle();
        float width = 400;
        textSTyle.fontSize = 30;

        if(_currentState.CurrentRootState != null)
            GUI.Label(new Rect(10, 10, 400, 50), $"Current Super State: {_currentState.CurrentRootState.ToString()}", textSTyle);
        
        if (_currentState.CurrentSubState != null)
            GUI.Label(new Rect((Screen.width / 2) - (width / 2), 10, width, 31), $"Current Sub State: {_currentState.CurrentSubState.ToString()}", textSTyle);
    }

    private void OnDeath(PlayerOnDeathSignal signal)
    {
        _currentState = _states.OnDeath();
        _currentState.EnterState();
    }

    private void OnPlayerAttackAnimationComplete(PlayerAttackAnimationCompleteSignal signal)
    {
        _currentState.CurrentSubState.PlayerOnAttackAnimationComplete();
    }
}
