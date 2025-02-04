using UnityEngine;
using VContainer;

public class CursorController : MonoBehaviour
{
    [Inject] GameManager _gameManager;

    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<ChangeCursorVisibilitySignal>(ChangeCursorVisibility);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MakeVisible()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MakeInvisible()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ChangeCursorVisibility(ChangeCursorVisibilitySignal signal)
    {
        Cursor.lockState = signal.Value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = signal.Value ? true : false;
    }

}
