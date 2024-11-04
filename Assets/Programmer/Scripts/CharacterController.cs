using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private IControlable _iMoveInpits;
    private IHealth _iHealth;
    protected PlayerInputs _playerInputs;

    protected virtual void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();

        _iMoveInpits = GetComponent<IControlable>();
    }

    private void Start()
    {
        _iHealth = GetComponent<PlayerStateMachine>().PayerHealth;
    }

    protected void Update() => ReadMovement();

    protected virtual void ReadMovement()
    {
        Vector2 inputDirection = _playerInputs.Gameplay.Movement.ReadValue<Vector2>();
        _iMoveInpits.MoveInput(inputDirection.x);
    }

    protected virtual void OnMedKitPerformed(InputAction.CallbackContext context) => _iHealth.ImproveHealth();
    protected virtual void OnJumpPressed(InputAction.CallbackContext context) => _iMoveInpits.JumpIsPressed();
    protected virtual void OnJumpReleased(InputAction.CallbackContext context) => _iMoveInpits.JumpIsReleased();

    protected void OnEnable()
    {
        _playerInputs.Gameplay.MedKitPerformed.performed += OnMedKitPerformed;
        _playerInputs.Gameplay.JumpIsPressed.performed += OnJumpPressed;
        _playerInputs.Gameplay.JumpIsReleased.performed += OnJumpReleased;
    }
    protected void OnDisable()
    {
        _playerInputs.Gameplay.MedKitPerformed.performed -= OnMedKitPerformed;
        _playerInputs.Gameplay.JumpIsPressed.performed -= OnJumpPressed;
        _playerInputs.Gameplay.JumpIsReleased.performed -= OnJumpReleased;
    }
}