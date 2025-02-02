using UnityEngine;

public class KeyUI : MonoBehaviour
{
    private EventBus _eventBus;

    private void Awake()
    {
        _eventBus = GetComponentInParent<MainCanvasManager>().EventBus;
        _eventBus.Subscribe<OpenDoorSignal>(OnDoorOpen);
    }

    private void OnDoorOpen(OpenDoorSignal signal)
    {
        Destroy(gameObject);
    }
}
