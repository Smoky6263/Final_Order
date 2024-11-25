using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private IControlable _iControlable;
    private IPlayerHealth _iHealth;
    protected PlayerInputs _playerInputs;

    protected virtual void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();

        _iControlable = GetComponent<IControlable>();
    }

    private void Start()
    {
        _iHealth = GetComponent<PlayerStateMachine>().PayerHealth;
    }

    private void Update() => ReadMovement();

    private void ReadMovement()
    {

        Vector2 inputDirection = _playerInputs.PlayerActions.Movement.ReadValue<Vector2>();
        _iControlable.MoveInput(inputDirection.x, inputDirection.y);
    }

    private void OnMedKitPerformed(InputAction.CallbackContext context) => _iHealth.ImproveHealth();
    private void OnJumpPressed(InputAction.CallbackContext context) => _iControlable.JumpIsPressed();
    private void OnJumpReleased(InputAction.CallbackContext context) => _iControlable.JumpIsReleased();
    private void OnRollPerformed(InputAction.CallbackContext context) => _iControlable.RollPressed();
    private void OnAttackPerforrmed(InputAction.CallbackContext context) => _iControlable.AttackPressed();

    protected void OnEnable()
    {
        _playerInputs.PlayerActions.MedKitPerformed.performed += OnMedKitPerformed;
        _playerInputs.PlayerActions.JumpIsPressed.performed += OnJumpPressed;
        _playerInputs.PlayerActions.JumpIsReleased.performed += OnJumpReleased;
        _playerInputs.PlayerActions.RollPerformed.performed += OnRollPerformed;
        _playerInputs.PlayerActions.AttackPressed.performed += OnAttackPerforrmed;
    }
    private void OnDisable()
    {
        _playerInputs.PlayerActions.MedKitPerformed.performed -= OnMedKitPerformed;
        _playerInputs.PlayerActions.JumpIsPressed.performed -= OnJumpPressed;
        _playerInputs.PlayerActions.JumpIsReleased.performed -= OnJumpReleased;
        _playerInputs.PlayerActions.RollPerformed.performed -= OnRollPerformed;
        _playerInputs.PlayerActions.AttackPressed.performed -= OnAttackPerforrmed;
    }
}