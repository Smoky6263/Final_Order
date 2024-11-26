using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private EventBusManager _eventBusManager;
    [SerializeField] private int _nextLevel;

    public EventBus EventBus { get; private set; }
    public int NextLevel { get { return _nextLevel; } }

    private void Awake()
    {
        EventBus = _eventBusManager.EventBus;
    }

    public void SpawnUIElement(GameObject uiElement)
    {
        Instantiate(uiElement, transform);
    }
}
