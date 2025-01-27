using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SoundSlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private SoundType _volumeType;
    [SerializeField] private TextMeshProUGUI _persentText;
    [SerializeField] private GameManager _gameManager;
    private EventBus _eventBus;


    private void Awake ()
    {
        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<SliderValueSetSignal>(OnSliderValueSet);

        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    public void OnEnable ()
    {
        _eventBus.Invoke(new SliderEnableSignal(_volumeType));
    }


    private void OnSliderValueChanged (float value)
    {
        if (_persentText != null) {
            float percentage = value * 100.0f;
            _persentText.text = $"{percentage:0}%";
        }

        _eventBus.Invoke(new SliderValueChangeSignal(_volumeType, value));
    }


    private void OnSliderValueSet (SliderValueSetSignal signal)
    {
        if (signal.SliderType == _volumeType) {
            _slider.value = signal.Value;

            if (_persentText != null) {
                float percentage = signal.Value * 100.0f;
                _persentText.text = $"{percentage:0}%";
            }
        }
    }
}
