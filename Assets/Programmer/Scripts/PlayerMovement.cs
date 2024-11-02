using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveInputs
{
    [SerializeField] private PlayerMovementStats _moveStats;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _feetColl;

    private Rigidbody2D _rigidBody;
    
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
    public float VerticalVelocity { get; private set; }
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

    private void Awake()
    {
        _isFacingRight = true;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CountTimers();
        JumpChecks();
    }

    private void FixedUpdate()
    {
        CollisionChecks();
        Jump();

        if (_isGrounded)
            Move(_moveStats.GroundAcceleration, _moveStats.GroundDeceleration, _movementInput);
        else
            Move(_moveStats.AirAcceleration, _moveStats.AirDeceleration, _movementInput);
    }
    
    #region PlayerInputs
    public void MoveInput(float x) => _movementInput.x = x;
    public void JumpIsPressed() => _jumpButtonPressed = true;
    public void JumpIsReleased() => _jumpButtonPressed = false;
    #endregion

    #region CollisionChecks

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.center.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, _moveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, _moveStats.GroundDetectionRayLength, _moveStats.GroundLayer);

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

    private void BumpHead()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x * _moveStats.HeadWidth, _moveStats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, _moveStats.HeadDetectionRayLength, _moveStats.GroundLayer);

        if (_headHit.collider != null)
            _bumpedHead = true;

        else { _bumpedHead = false; }

        #region DebugVisualization

        if (_moveStats.DebugShowHeadBumpBox)
        {
            Color rayColor;

            if (_bumpedHead)
                rayColor = Color.green;

            else { rayColor = Color.red; }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * _moveStats.HeadWidth, boxCastOrigin.y), Vector2.up * _moveStats.HeadDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + (boxCastSize.x / 2) * _moveStats.HeadWidth, boxCastOrigin.y), Vector2.up * _moveStats.HeadDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * _moveStats.HeadWidth, boxCastOrigin.y + _moveStats.HeadDetectionRayLength), Vector2.right * boxCastSize.x *_moveStats.HeadWidth, rayColor);

            #endregion
        }
    }

    private void CollisionChecks()
    {
        IsGrounded();
        BumpHead();
    }

    #endregion
    
    #region Movement
    private void Move(float acceleration, float deceleration, Vector2 movementInput)
    {

        if (movementInput != Vector2.zero)
        {
            TurnCheck(movementInput);

            Vector2 targetVelocity = Vector2.zero;
            targetVelocity = new Vector2(movementInput.x, 0f) * _moveStats.MaxRunSpeed;

            _movementVelocity = Vector2.Lerp(_movementVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            _rigidBody.velocity = new Vector2(_movementVelocity.x, _rigidBody.velocity.y);
        }

        else if(movementInput == Vector2.zero) 
        {
            _movementVelocity = Vector2.Lerp(_movementVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            _rigidBody.velocity = new Vector2(_movementVelocity.x, _rigidBody.velocity.y);
        }
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0)
            Turn(false);

        else if (!_isFacingRight && moveInput.x > 0)
            Turn(true);
    }
    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            _spriteRenderer.flipX = false;
        }
        else
        {
            _isFacingRight = false;
            _spriteRenderer.flipX = true;
        }
    }
    #endregion
    
    #region Jump

    private void JumpChecks()
    {
        //WHEN WE PRESS THE JUMP BUTTON
        if (_jumpButtonPressed)
        {
            _jumpBufferTimer = _moveStats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
        }

        //WHEN WE RELEASE THE JUMP BUTTON
        if (!_jumpButtonPressed)
        {
            if (_jumpBufferTimer > 0f)
                _jumpReleasedDuringBuffer = true;

            if(_isJumping && VerticalVelocity > 0f)
            {
                if (_isPastApexThreshold)
                {
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallTime = _moveStats.TimeForUpwardsCancel;
                    VerticalVelocity = 0f;
                }
                else
                {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }
            }
        }

        //INITIATE JUMP WITH BUFFERING AND COYOTE TIME
        if(_jumpBufferTimer > 0 && !_isJumping && (_isGrounded || _coyoteTimer > 0f))
        {
            InitiateJump();

            if (_jumpReleasedDuringBuffer)
            {
                _isFastFalling = true;
                _fastFallReleaseSpeed = VerticalVelocity;
            }
        }

        //AIR JUMP AFTER COYOTE TIME LAPSED
        else if (_jumpBufferTimer > 0f && _isFalling)
        {
            _isFalling = false;
        }

        //LANDED
        if((_isJumping || _isFalling) && _isGrounded && VerticalVelocity <= 0f)
        {
            _isJumping = false;
            _isFalling = false;
            _isFastFalling = false;

            _fastFallTime = 0f;
            _jumpBufferTimer = 0f;
            _isPastApexThreshold = false;

            _jumpButtonPressed = false;
            VerticalVelocity = Physics2D.gravity.y;
        }
    }

    private void InitiateJump()
    {
        if(!_isJumping)
            _isJumping = true;

        _jumpBufferTimer = 0f;
        VerticalVelocity = _moveStats.InitialJumpVelocity;
    }

    private void Jump() 
    {
        //APPLY GRAVITY WHILE JUMPING
        if(_isJumping)
        {
            //CHECK FOR HEAD BUMP
            if (_bumpedHead)
            {
                _isFastFalling = true;
            }

            //GRAVITY ON ASCENDING
            if(VerticalVelocity >= 0)
            {
                //APEX CONTROLS
                _apexPoint = Mathf.InverseLerp(_moveStats.InitialJumpVelocity, 0f, VerticalVelocity);

                if(_apexPoint > _moveStats.ApexThreshold)
                {
                    if(!_isPastApexThreshold)
                    {
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }

                    if (_isPastApexThreshold)
                    {
                        _timePastApexThreshold += Time.fixedDeltaTime;
                        if(_timePastApexThreshold < _moveStats.ApexHangTime)
                        {
                            VerticalVelocity = 0f;
                        }
                        else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                }

                //GRAVITY ON ASCENDING BUT NOT PAST APEX TRESHOLD
                else 
                {
                    VerticalVelocity += _moveStats.Gravity * Time.deltaTime;
                    if (_isPastApexThreshold)
                    {
                        _isPastApexThreshold = false;
                    }
                }
            }

            //GRAVITY ON DESCENDING 
            else if (!_isFastFalling)
            {
                VerticalVelocity += _moveStats.Gravity * _moveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if(VerticalVelocity < 0f)
            {
                if(!_isFalling)
                {
                    _isFalling = true;
                }
            }
        }

        //JUMP CUT
        if (_isFastFalling)
        {
            if(_fastFallTime >= _moveStats.TimeForUpwardsCancel)
            {
                VerticalVelocity += _moveStats.Gravity * _moveStats.GravityOnReleaseMultiplier * Time.deltaTime;
            }
            else if( _fastFallTime < _moveStats.TimeForUpwardsCancel)
            {
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallTime / _moveStats.TimeForUpwardsCancel));
            }

            _fastFallTime += Time.deltaTime;
        }
        
        //NORMAL GRAVITY WHILE FALLING
        if(!_isGrounded && !_isJumping)
        {
            if (!_isFalling)
            {
                _isFalling = true;
            }

            VerticalVelocity += _moveStats.Gravity * Time.deltaTime;
        }

        //CLAMP FALLS SPEED
        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -_moveStats.MaxFallSpeed, 50f);

        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, VerticalVelocity);
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

    private void DrawJumpArc(float moveSpeed, Color gizmoColor)
    {
        Vector2 startPosition = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 previousPosition = startPosition;
        float speed = 0f;

        if (_moveStats.DrawRight)
        {
            speed = moveSpeed;
        }
        else
        {
            speed = -moveSpeed;
        }

        Vector2 velocity = new Vector2(speed, _moveStats.InitialJumpVelocity);

        Gizmos.color = gizmoColor;

        float timeStep = 2 * _moveStats.TimeTillJumpApex / _moveStats.ArcResolution; // time step for the simulation
        float totalTime = 2 * _moveStats.TimeTillJumpApex + _moveStats.ApexHangTime; // total time of the arc including hang time

        for (int i = 0; i < _moveStats.VisualizationSteps; i++)
        {
            float simulationTime = i * timeStep;
            Vector2 displacement;
            Vector2 drawPoint;

            if (simulationTime < _moveStats.TimeTillJumpApex) // Ascending
            {
                displacement = velocity * simulationTime + 0.5f * new Vector2(0, _moveStats.Gravity) * simulationTime * simulationTime;
            }
            else if (simulationTime < _moveStats.TimeTillJumpApex + _moveStats.ApexHangTime) // Apex hang time
            {
                float apexTime = simulationTime - _moveStats.TimeTillJumpApex;
                velocity = new Vector2(speed, 0);
                displacement = velocity * simulationTime + 0.5f * new Vector2(0, _moveStats.Gravity) * _moveStats.TimeTillJumpApex * _moveStats.TimeTillJumpApex;
                displacement += new Vector2(speed, 0) * apexTime; // No vertical movement during hang time
            }
            else // Descending
            {
                float descendTime = simulationTime - (_moveStats.TimeTillJumpApex + _moveStats.ApexHangTime);
                displacement = velocity * _moveStats.TimeTillJumpApex + 0.5f * new Vector2(0, _moveStats.Gravity) * _moveStats.TimeTillJumpApex * _moveStats.TimeTillJumpApex;
                displacement += new Vector2(speed, 0) * _moveStats.ApexHangTime; // Horizontal movement during hang time
                displacement += new Vector2(0, 0.5f * _moveStats.Gravity * descendTime * descendTime);
            }

            drawPoint = startPosition + displacement;

            if (_moveStats.StopOnCollision)
            {
                RaycastHit2D hit = Physics2D.Raycast(previousPosition, drawPoint - previousPosition, Vector2.Distance(previousPosition, drawPoint), _moveStats.GroundLayer);
                if (hit.collider != null)
                {
                    // If a hit is detected, stop drawing the arc at the hit point
                    Gizmos.DrawLine(previousPosition, hit.point);
                    break;
                }
            }

            Gizmos.DrawLine(previousPosition, drawPoint);
            previousPosition = drawPoint;
        }
    }
    private void OnDrawGizmos()
    {
        if (_moveStats.ShowRunJumpArc)
            DrawJumpArc(_moveStats.MaxRunSpeed, Color.red);
    }
}
