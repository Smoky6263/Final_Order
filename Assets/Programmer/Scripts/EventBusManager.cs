using UnityEngine;

public class EventBusManager : MonoBehaviour
{
    public EventBus EventBus { get; } = new();
}
