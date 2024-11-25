using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private EventBus _eventBus;

    private PlayerInputs _inputs;

    private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

    public bool OnPause { get; private set; } = false;

    private void Awake()
    {
        _eventBus = GetComponent<EventBusManager>().EventBus;
        _eventBus.Subscribe<OnPauseEventSignal>(OnPauseEvent);

        _inputs = new PlayerInputs();
        _inputs.Enable();
    }

    public void OnPauseEvent(OnPauseEventSignal signal) => SetPause();

    private void OnPausePress(InputAction.CallbackContext context)
    {
        if (OnPause == false) SetPause();
        
        else SetPlay();
    }

    private void SetPause()
    {
        foreach (IPauseHandler handler in _pauseHandlers)
        {
            handler.SetPause();
        }

        OnPause = true;
    }

    private void SetPlay()
    {
        foreach (IPauseHandler handler in _pauseHandlers)
        {
            handler.SetPlay();
        }

        OnPause = false;
    }

    public void Register(IPauseHandler pauseHandler)
    {
        if (_pauseHandlers.Contains(pauseHandler) == false)
        {
            _pauseHandlers.Add(pauseHandler);
            Debug.Log($"Объект: {pauseHandler} зарегистрирован в менджерепаузы");
        }
        else
        {
            Debug.LogWarning($"Попытка зарегистрировать объект: {pauseHandler}, который уже есть в пазменеджере!");
        }
    }

    public void Unregister(IPauseHandler pauseHandler)
    {
        if (_pauseHandlers.Contains(pauseHandler))
        {
            _pauseHandlers.Remove(pauseHandler);
            Debug.Log($"{pauseHandler} удалён из списка обработчиков.");
        }
        else
        {
            Debug.LogWarning($"{pauseHandler} не найден в списке.");
        }
    }

    private void OnEnable()
    {
        _inputs.UIActions.CallPauseMenu.performed += OnPausePress;
    }
    private void OnDisable()
    {
        _inputs.UIActions.CallPauseMenu.performed -= OnPausePress;
    }
}
