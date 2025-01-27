using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SoundSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _persentText;
    private Slider _slider;
    [SerializeField] private SoundSaveSystemController _soundSaveSystemController;
    [SerializeField] private SoundType _volumeType;

    private float _startMasterVolume;
    private float _startMusicVolume;
    private float _startSfxVolume;

    private SoundBusManager _soundBusManager;

    public void OnEnable ()
    {
        _soundBusManager = _soundSaveSystemController.SoundBusManager;
        _slider = GetComponent<Slider>();

        if (_soundBusManager != null) {
            switch (_volumeType) {
                case SoundType.Master:
                    float masterValue = _soundBusManager.GetMasterVolume();
                    _slider.value = masterValue;
                    _startMasterVolume = masterValue;
                    OnSliderValueChanged(masterValue);
                    break;
                case SoundType.Music:
                    float musicValue = _soundBusManager.GetMusicVolume();
                    _slider.value = musicValue;
                    _startMusicVolume = musicValue;
                    OnSliderValueChanged(musicValue);
                    break;
                case SoundType.SFX:
                    float sfxValue = _soundBusManager.GetSFXVolume();
                    _slider.value = sfxValue;
                    _startSfxVolume = sfxValue;
                    OnSliderValueChanged(sfxValue);
                    break;
            }
        }

        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged (float value)
    {
        float percentage = value * 100.0f;
        if (_persentText != null) {
            _persentText.text = $"{percentage:0}%";
        }

        if (_soundBusManager != null) {
            switch (_volumeType) {
                case SoundType.Master:
                    _soundBusManager.SetMasterVolume(value);
                    break;
                case SoundType.Music:
                    _soundBusManager.SetMusicVolume(value);
                    break;
                case SoundType.SFX:
                    _soundBusManager.SetSFXVolume(value);
                    break;
            }
        }
    }


    public float GetSliderValue ()
    {
        float value = _slider.value / _slider.maxValue;
        return Mathf.Round(value * 100f);
    }
}
