using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Windows;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private EventBus _eventBus;
    private PlayerInputs _inputs;
    private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

    public bool OnPause { get; private set; } = false;

    private void Awake()
    {
        _eventBus = GetComponent<GameManager>().EventBus;
        _eventBus.Subscribe<OnPauseEventSignal>(OnPauseEvent);

        _inputs = new PlayerInputs();
        _inputs.Enable();
    }

    public void OnPauseEvent(OnPauseEventSignal signal)
    {
        if (signal.OnPause)
        {
            _inputs.Disable();
            SetPause();
        }
        else
        {
            _inputs.Enable();
            SetPlay();
        }

    }

    private void OnPausePress(InputAction.CallbackContext context)
    {
        if (OnPause == false) SetPause();
        
        else SetPlay();
    }

    private void SetPause()
    {
        foreach (IPauseHandler handler in _pauseHandlers)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Pause", 1);
            _pauseMenu.gameObject.SetActive(true);
            _inputs.Disable();
            handler.SetPause();
        }
        _eventBus.Invoke(new ChangeCursorVisibilitySignal(true));

        OnPause = true;
    }

    private void SetPlay()
    {
        foreach (IPauseHandler handler in _pauseHandlers)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Pause", 0);
            //_soundSaveSystemController.UpdateSoundData();
            _pauseMenu.gameObject.SetActive(false);
            _inputs.Enable();
            handler.SetPlay();
        }

        _eventBus.Invoke(new ChangeCursorVisibilitySignal(false));

        OnPause = false;
    }

    public void Register(IPauseHandler pauseHandler)
    {
        if (_pauseHandlers.Contains(pauseHandler) == false)
        {
            _pauseHandlers.Add(pauseHandler);
            //Debug.Log($"Объект: {pauseHandler} зарегистрирован в менджерепаузы");
        }
        else
        {
            //Debug.LogWarning($"Попытка зарегистрировать объект: {pauseHandler}, который уже есть в пазменеджере!");
        }
    }

    public void Unregister(IPauseHandler pauseHandler)
    {
        if (_pauseHandlers.Contains(pauseHandler))
        {
            _pauseHandlers.Remove(pauseHandler);
            //Debug.Log($"{pauseHandler} удалён из списка обработчиков.");
        }
        else
        {
            //Debug.LogWarning($"{pauseHandler} не найден в списке.");
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
