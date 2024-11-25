using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    
    private EventBus _eventBus;
    private Slider _slider;

    private void Start()
    {
        _eventBus = GetComponentInParent<MainCanvasManager>().EventBus;
        _eventBus.Subscribe<PlayerHealthChangeSignal>(OnHealthChanged);

        _slider = GetComponent<Slider>();
    }

    private void OnHealthChanged(PlayerHealthChangeSignal signal)
    {
        _slider.value = signal.Health;
        Debug.Log(signal.Health);
    }
}
