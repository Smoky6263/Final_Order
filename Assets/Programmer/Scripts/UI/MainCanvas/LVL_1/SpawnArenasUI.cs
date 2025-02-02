using UnityEngine;
using VContainer;

public class SpawnArenasUI : MonoBehaviour
{
    [Inject] private MainCanvasManager _mainCanvasManager;

    [SerializeField] private GameObject _keyUI;
    [SerializeField] private GameObject _welcomeToArenaText;
    [SerializeField] private GameObject _arenaWaweText;

    [Inject] private GameManager _gameManager;
    
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<OnKeyPickedUp>(SpawnKeyUnUI);
    }

    private void SpawnKeyUnUI(OnKeyPickedUp up)
    {
        _mainCanvasManager.SpawnUIElement(_keyUI);
    }

    public void SpawnArenaText()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fight", 1);
        _mainCanvasManager.SpawnUIElement(_welcomeToArenaText);
    }
}
