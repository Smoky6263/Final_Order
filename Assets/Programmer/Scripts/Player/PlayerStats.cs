using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovementStats")]
public class PlayerStats : ScriptableObject
{

    [Header("Weapon")]
    [SerializeField, Range(0f,100f)] public float _weaponDamage;

    [Header("Duration Stats")]
    [Range(0f, 5f)] public float RollDuration = 1f;

    [Header("Run")]
    [Range(1f, 100f)] public float MaxRunSpeed = 12.5f;
    [Range(0.25f, 50f)] public float GroundAcceleration = 5f;
    [Range(0.25f, 50f)] public float GroundDeceleration = 20f;
    [Range(0.25f, 50f)] public float AirAcceleration = 5f;
    [Range(0.25f, 50f)] public float AirDeceleration = 5f;

    [Header("Grounded/Collision Checks")]
    public LayerMask JumpSurfaceLayer;
    public LayerMask HeadBumpSurfaceLayer;
    public LayerMask OnStairsLayer;
    public float GroundDetectionRayLength = 0.02f;
    public float HeadDetectionRayLength = 0.02f;
    [Range(0f, 1f)] public float HeadWidth = 0.75f;

    [Header("jump")]
    public float JumpHeight = 6.5f;
    [Range(1f, 1.1f)] public float JumpHeightCompensationFactor = 1.054f;
    public float TimeTillJumpApex = 0.35f;
    [Range(.01f, 5f)] public float GravityOnReleaseMultiplier = 2f;
    public float MaxFallSpeed = 26f;

    [Header("jump after stairs")]
    [Range(0f, 30f)] public float InitialJumpFromStairsVelocity = 5f;
    [Range(0f, 5f)] public float JumpfAfterStairsDuration = 1f;

    [Header("Jump Cut")]
    [Range(0.02f, 0.3f)] public float TimeForUpwardsCancel = 0.027f;

    [Header("Jump Apex")]
    [Range(0.5f, 1f)] public float ApexThreshold = 0.97f;
    [Range(0.01f, 1f)] public float ApexHangTime = 0.97f;

    [Header("Jump Buffer")]
    [Range(0.0f, 1f)] public float JumpBufferTime = 0.125f;

    [Header("Jump Coyote Time")]
    [Range(0.0f, 1f)] public float JumpCoyoteTime = 0.125f;

    [Header("Draw debug rays?")]
    public bool DebugShowIsGroundedBox;
    public bool DebugShowHeadBumpBox;

    [Header("Jump Visualization Tool")]
    public bool ShowWalkJumpArc = false;
    public bool ShowRunJumpArc = false;
    public bool StopOnCollision = true;
    public bool DrawRight = true;
    [Range(5, 100)] public int ArcResolution = 20;
    [Range(0, 500)] public int VisualizationSteps = 90;

    public float Gravity { get; private set; }
    public float InitialJumpVelocity { get; private set; }
    public float AdjustedJumpHeight { get; private set; }

    private void OnValidate()
    {
        CalculateValues();
    }

    private void OnEnable()
    {
        CalculateValues();
    }

    private void CalculateValues()
    {
        AdjustedJumpHeight = JumpHeight * JumpHeightCompensationFactor;
        Gravity = -(2f * AdjustedJumpHeight) / Mathf.Pow(TimeTillJumpApex, 2f);
        InitialJumpVelocity = Mathf.Abs(Gravity) * TimeTillJumpApex;
    }
}
