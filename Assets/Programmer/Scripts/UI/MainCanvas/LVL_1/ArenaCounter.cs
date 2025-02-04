using TMPro;
using UnityEngine;

public class ArenaCounter : MonoBehaviour
{
    private EventBus _eventBus;

    [SerializeField] private TextMeshProUGUI _vaweCounter;
    [SerializeField] private TextMeshProUGUI _enemyCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _eventBus = GetComponentInParent<MainCanvasManager>().EventBus;
        _eventBus.Subscribe<OnVaweCountUpdtaeSignal>(UpdateVaweCount);
        _eventBus.Subscribe<OnEnemyCountUpdateSignal>(UpdateEnemyCount);
        _eventBus.Subscribe<OnArenaPassedSignal>(DoFadeOut);
    }

    private void UpdateVaweCount(OnVaweCountUpdtaeSignal signal)
    {
        _vaweCounter.text = $"Wave: {signal.Value}";
    }

    private void UpdateEnemyCount(OnEnemyCountUpdateSignal signal)
    {
        _enemyCounter.text = $"Enemies: {signal.Value}";
    }

    private void DoFadeOut(OnArenaPassedSignal signal)
    {
        _animator.Play("FadeOut");
    }

    public void DestroyGameObject()
    {
        _eventBus.Unsubscribe<OnVaweCountUpdtaeSignal>(UpdateVaweCount);
        _eventBus.Unsubscribe<OnEnemyCountUpdateSignal>(UpdateEnemyCount);
        _eventBus.Unsubscribe<OnArenaPassedSignal>(DoFadeOut);

        Destroy(gameObject);
    }

}
