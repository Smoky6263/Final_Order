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

    protected void OnEnable() => _playerInputs.Gameplay.DoJump.performed += OnJumpPerformed;
    protected void OnDisable() => _playerInputs.Gameplay.DoJump.performed -= OnJumpPerformed;
}