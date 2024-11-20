using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private EventBusManager _eventBusManager;
    
    private EventBus _eventBus;
    private Slider _slider;

    private void Awake()
    {
        if (_eventBusManager == null)
            Debug.LogWarning($"Ты забыл прокинуть ссылки в инспекторе на обьект {this.gameObject.name}!");

        _eventBus = _eventBusManager.EventBus;
        _eventBus.Subscribe<PlayerHealthChangeSignal>(OnHealthChanged);
        _slider = GetComponent<Slider>();
    }

    private void OnHealthChanged(PlayerHealthChangeSignal signal)
    {
        _slider.value = signal.Health;
        Debug.Log(signal.Health);
    }
}
