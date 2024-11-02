using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats _moveStats;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _feetColl;

    private Rigidbody2D _rigidBody;
    
    //player inputs
    private Vector2 _movementInput;

    //movement vars
    private Vector2 _movementVelocity;
    private bool _isFacingRight;

    //collision check vars

    //jump vars
    public float VerticalVelocity { get; private set; }

    private void FixedUpdate()
    {

            Move(_moveStats.GroundAcceleration, _moveStats.GroundDeceleration, _movementInput);
            Move(_moveStats.AirAcceleration, _moveStats.AirDeceleration, _movementInput);
    }
    
    #region Movement
    private void Move(float acceleration, float deceleration, Vector2 movementInput)
    {

        if (movementInput != Vector2.zero)
        {

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
