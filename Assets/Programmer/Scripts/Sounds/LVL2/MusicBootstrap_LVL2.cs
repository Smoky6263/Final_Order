using UnityEngine;
using VContainer;

[RequireComponent(typeof(InjectableGameObject))]
public class MusicBootstrap_LVL2 : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    private EventBus _eventBus;
    private void Start()
    {
        _eventBus = _gameManager.EventBus;
        _eventBus.Invoke(new FMODParameterChangeSignal("Boss", 0f));
    }
}
