using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private IControlable _iControlable;
    protected PlayerInputs _playerInputs;

    protected virtual void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();

        _iControlable = GetComponent<IControlable>();
    }

    protected void FixedUpdate() => ReadMovement();

    protected virtual void ReadMovement()
    {
        Vector2 inputDirection = _playerInputs.Gameplay.Movement.ReadValue<Vector2>();
        _iControlable.MovePerformed(inputDirection.x);
    }

    protected virtual void OnJumpPerformed(InputAction.CallbackContext context) => _iControlable.JumpPerformed();
    protected virtual void OnMedKitPerformed(InputAction.CallbackContext context) => _iControlable.MedKitPerformed();

    protected void OnEnable()
    {
        _playerInputs.Gameplay.MedKitPerformed.performed += OnMedKitPerformed;
        _playerInputs.Gameplay.JumpPerformed.performed += OnJumpPerformed;
    }
    protected void OnDisable()
    {
        _playerInputs.Gameplay.MedKitPerformed.performed -= OnMedKitPerformed;
        _playerInputs.Gameplay.JumpPerformed.performed -= OnJumpPerformed;
    }
}