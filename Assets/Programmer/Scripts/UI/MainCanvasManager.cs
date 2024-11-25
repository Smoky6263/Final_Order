using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private EventBusManager _eventBusManager;

    public EventBus EventBus { get; private set; }

    private void Awake()
    {
        EventBus = _eventBusManager.EventBus;
    }

    public void SpawnUIElement(GameObject uiElement)
    {
        Instantiate(uiElement, transform);
    }
}
