using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PausePanel : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    [SerializeField] private List<GameObject> _childPanels;

    private EventBus _eventBus;
    private PlayerInputs _playerInputs;

    private int _menuDepth;

    private void Awake()
    {
        _eventBus = _gameManager.EventBus;
        _playerInputs = new PlayerInputs();

        foreach (var item in _childPanels)
            item.SetActive(false);

        gameObject.SetActive(false);
    }

    private void EscapePressed(InputAction.CallbackContext context)
    {
        if (_menuDepth > 0)
        {
            _childPanels[_menuDepth].SetActive(false);
            _childPanels[_menuDepth - 1].SetActive(true);
            _menuDepth--;
            return;
        }

        _eventBus.Invoke(new OnPauseEventSignal(false));
        gameObject.SetActive(false);
    }
    public void EscapePressed()
    {
        _eventBus.Invoke(new OnPauseEventSignal(false));
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _childPanels[0].SetActive(true);
        _playerInputs.UIActions.CallPauseMenu.performed += EscapePressed;
        _playerInputs.Enable();
        
        _menuDepth = 0;
    }
    private void OnDisable()
    {
        _playerInputs.UIActions.CallPauseMenu.performed -= EscapePressed;
        _playerInputs.Disable();

        foreach (var item in _childPanels)
            item.SetActive(false);
    }

    public void IncreaseDepth() => _menuDepth++;
    public void DecreaseDepth() => _menuDepth--;
}
