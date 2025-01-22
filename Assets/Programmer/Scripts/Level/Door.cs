using UnityEngine;

public class Door : MonoBehaviour
{
    private EventBus _eventBus;
    private Animator _animator;
    public int DoorCloseHash { get; private set; }

    private void Start()
    {
        _eventBus = GameManager.Instance.EventBus;
        _eventBus.Subscribe<CloseDoorSignal>(CloseDoor);
        _animator = GetComponent<Animator>();
        DoorCloseHash = Animator.StringToHash("DoorClose");
    }

    private void CloseDoor(CloseDoorSignal signal) => _animator.Play(DoorCloseHash);
}
