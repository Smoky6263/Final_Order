using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimatorController))]
[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour, IControlable
{
    private EventBus _eventBus;
    private PlayerHealth _playerHealth;
    private Rigidbody2D _rigidBody;

    private PlayerAnimatorController _playerAnimatorController;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerStats _moveStats;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _feetColl;

    #region Player Fields

    [Header("Health")]
    [SerializeField, Range(0f, 100f)] public float _maxHealth;
    [SerializeField, Range(0f, 100f)] public float _health;

    private bool _onStairs;
    private bool _onPassTroughPlatform;

    #endregion

    #region Move Fields
    //player inputs
    private Vector2 _movementInput;
    private bool _jumpButtonPressed;

    //movement vars
    private Vector2 _movementVelocity;
    private bool _isFacingRight;

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
    public PlayerStats MoveStats { get { return _moveStats; } }
    public Collider2D BodyColl { get { return _bodyColl; } }
    public Collider2D FeetColl { get { return _feetColl; } }
    public Rigidbody2D RigidBody { get { return _rigidBody; } }
    public PlayerAnimatorController PlayerAnimatorController { get { return _playerAnimatorController; } }
    public EventBus EventBus { get { return _eventBus; } }
    public PlayerHealth PayerHealth { get { return _playerHealth; } }
    
    //player inputs

    public Vector2 MovementInput { get { return _movementInput; } }
    public bool JumpButtonPressed { get { return _jumpButtonPressed; } set { _jumpButtonPressed = value; } }

    //collision check vars
    public RaycastHit2D GroundHit { get { return _groundHit; } set { _groundHit = value; } }
    public RaycastHit2D HeadHit { get { return _headHit; } set { _headHit = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool BumpedHead {  get { return _bumpedHead; } set { _bumpedHead = value; } }
    public bool OnStairs { get { return _onStairs; } set { _onStairs = value; } }
    public bool OnPassTroughPlatform { get { return _onPassTroughPlatform; } set { _onPassTroughPlatform = value; } }

    #region Movement and Jump Felids

    //on stairs bool

    //movement vars
    public Vector2 MovementVelocity { get { return _movementVelocity; } set { _movementVelocity = value; } }
    public bool IsFacingRight {  get { return _isFacingRight; } set { _isFacingRight = value; } }

    //jump vars
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public bool IsFastFalling { get { return _isFastFalling; } set { _isFastFalling = value; } }
    public float FastFallReleaseSpeed { get { return _fastFallReleaseSpeed; } set { _fastFallReleaseSpeed = value; } }
    public bool IsFalling { get { return _isFalling; } set { _isFalling = value; } }
    public float FastFallTime { get { return _fastFallTime; } set { _fastFallTime = value; } }

    //apex vars
    public float ApexPoint { get { return _apexPoint; } set { _apexPoint = value; } }
    public float TimePastApexThreshold { get {  return _timePastApexThreshold; } set { _timePastApexThreshold = value; } }
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
        _playerHealth = new PlayerHealth(this);
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimatorController = GetComponent<PlayerAnimatorController>();
        _isFacingRight = true;
    }

    private void Start()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState(); 
    }

    private void Update()
    {
        CountTimers();
        IsGroundedCheck();
    }

    private void FixedUpdate()
    {
        _currentState.UpdateStates(); 
    }

    #region PlayerInputs
    public void MoveInput(float x, float y) => _movementInput = new Vector2(x , y);
    public void JumpIsPressed() => _jumpButtonPressed = true;
    public void JumpIsReleased() => _jumpButtonPressed = false;
    #endregion

#region Collision Checks
    private void IsGroundedCheck()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.center.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, _moveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, _moveStats.GroundDetectionRayLength, _moveStats.GroundLayer);
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

    #region Timers
    private void CountTimers()
    {
        _jumpBufferTimer -= Time.deltaTime;

        if (!_isGrounded)
            _coyoteTimer -= Time.deltaTime;
        else
            _coyoteTimer = _moveStats.JumpCoyoteTime;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Stairs")
            _onStairs = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Stairs")
            _onStairs = false;

    }

    private void OnGUI()
    {
        GUIStyle textSTyle = new GUIStyle();
        float width = 400;
        textSTyle.fontSize = 30;

        if(_currentState.CurrentSuperState != null)
            GUI.Label(new Rect(10, 10, 400, 50), $"Current Super State: {_currentState.CurrentSuperState.ToString()}", textSTyle);
        
        if (_currentState.CurrentSubState != null)
            GUI.Label(new Rect((Screen.width / 2) - (width / 2), 10, width, 31), $"Current Sub State: {_currentState.CurrentSubState.ToString()}", textSTyle);
    }
}