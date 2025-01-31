using UnityEngine;

public class Door : MonoBehaviour
{
    private EventBus _eventBus;
    private Animator _animator;
    public int DoorCloseHash { get; private set; }
    public int DoorOpenHash { get; private set; }

    private void Start()
    {
        _eventBus = GameManager.Instance.EventBus;
        _eventBus.Subscribe<OpenDoorSignal>(OpenDoor);
        _animator = GetComponent<Animator>();
        DoorCloseHash = Animator.StringToHash("DoorClose");
        DoorOpenHash = Animator.StringToHash("DoorOpen");
    }

    private void OpenDoor(OpenDoorSignal signal) => _animator.Play(DoorOpenHash);
    public void TurnOff() => gameObject.SetActive(false);
}
