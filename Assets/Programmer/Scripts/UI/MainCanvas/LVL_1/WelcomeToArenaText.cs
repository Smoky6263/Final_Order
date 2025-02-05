using UnityEngine;

public class WelcomeToArenaText : MonoBehaviour
{
    [SerializeField] private GameObject _vaweCounter;
    private EventBus _eventBus;

    private void Awake() => _eventBus = GetComponentInParent<MainCanvasManager>().EventBus;

    public void OnAnimationComplete()
    {
        _eventBus.Invoke(new SpawnUIElementSignal(_vaweCounter));
        _eventBus.Invoke(new OnArenaFightBeginSignal());
    }
}
