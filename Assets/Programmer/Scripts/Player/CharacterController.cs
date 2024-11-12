using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private IControlable _iControlable;
    private IHealth _iHealth;
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

    protected void Update() => ReadMovement();

    protected virtual void ReadMovement()
    {

        Vector2 inputDirection = _playerInputs.Gameplay.Movement.ReadValue<Vector2>();
        _iControlable.MoveInput(inputDirection.x, inputDirection.y);
    }

    protected virtual void OnMedKitPerformed(InputAction.CallbackContext context) => _iHealth.ImproveHealth();
    protected virtual void OnJumpPressed(InputAction.CallbackContext context) => _iControlable.JumpIsPressed();
    protected virtual void OnJumpReleased(InputAction.CallbackContext context) => _iControlable.JumpIsReleased();
    protected virtual void OnRollPerformed(InputAction.CallbackContext context) => _iControlable.RollPressed();


    protected void OnEnable()
    {
        _playerInputs.Gameplay.MedKitPerformed.performed += OnMedKitPerformed;
        _playerInputs.Gameplay.JumpIsPressed.performed += OnJumpPressed;
        _playerInputs.Gameplay.JumpIsReleased.performed += OnJumpReleased;
        _playerInputs.Gameplay.RollPerformed.performed += OnRollPerformed;
    }
    protected void OnDisable()
    {
        _playerInputs.Gameplay.MedKitPerformed.performed -= OnMedKitPerformed;
        _playerInputs.Gameplay.JumpIsPressed.performed -= OnJumpPressed;
        _playerInputs.Gameplay.JumpIsReleased.performed -= OnJumpReleased;
        _playerInputs.Gameplay.RollPerformed.performed -= OnRollPerformed;
    }
}