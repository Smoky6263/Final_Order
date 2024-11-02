using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    private EventBus _eventBus;
    private Slider _slider;

    private void Awake()
    {
        if (_gameManager == null)
            Debug.LogWarning($"Ты забыл прокинуть ссылки в инспекторе на обьект {this.gameObject.name}!");

        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<PlayerHealthChangeSignal>(OnHealthChanged);
        _slider = GetComponent<Slider>();
    }

    private void OnHealthChanged(PlayerHealthChangeSignal signal)
    {
        _slider.value = signal.Health;
        Debug.Log(signal.Health);
    }
}
