using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private EventBusManager _eventBusManager;

    public EventBus _eventBus { get; private set; }

    private void Awake()
    {
        _eventBus = GetComponent<EventBusManager>().EventBus;
    }

    public void SpawnUIElement(GameObject uiElement)
    {
        Instantiate(uiElement, transform);
    }
}
