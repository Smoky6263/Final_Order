using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class DoorOpenerTrigger : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    [SerializeField] private TextMeshProUGUI _topText;
    [SerializeField] private TextMeshProUGUI _bottomText;

    private EventBus _eventBus;

    private PlayerInputs _playerInputs;
    private SoundsController _soundController;

    private string _player = "PlayerMainTrigger";
    private bool _RMBPressed;
    private bool _KeyPickedUp;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _soundController = GetComponent<SoundsController>();
        _soundController.SoundsManager = _gameManager.GetComponent<SoundsManager>();
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<OnKeyPickedUp>(KeyPickedUp);
    }

    private void KeyPickedUp(OnKeyPickedUp signal)
    {
        _topText.text = "Press RMB";
        _bottomText.text = "to open the door";
        _KeyPickedUp = true;
    }


    private void KeyPressed(InputAction.CallbackContext context)
    {
        _RMBPressed = !_RMBPressed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _player && _KeyPickedUp && _RMBPressed)
        {
            _gameManager.EventBus.Invoke(new OpenDoorSignal());
            _soundController.MedkKitUse();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _player)
        _playerInputs.Enable();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _player)
            _playerInputs.Disable();
    }

    private void OnEnable() => _playerInputs.UseInteractable.Use.performed += KeyPressed;
    private void OnDisable() => _playerInputs.UseInteractable.Use.performed -= KeyPressed;

}
