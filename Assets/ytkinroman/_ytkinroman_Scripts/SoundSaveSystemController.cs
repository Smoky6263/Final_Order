using UnityEditor.Rendering;
using UnityEngine;


public class SoundSaveSystemController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private EventBus _eventBus;

    [SerializeField] private SoundSaveSystem _soudSaveSystem;

    public SoundBusManager _soundBusManager;


    public void Initialization ()
    {
        _soudSaveSystem.Initialization();

        _soundBusManager = new SoundBusManager();
        _soundBusManager.Initialization();
        _soundBusManager.SetMasterVolume(_soudSaveSystem._soundData.masterVolume / 100.0f);
        _soundBusManager.SetMusicVolume(_soudSaveSystem._soundData.musicVolume / 100.0f);
        _soundBusManager.SetSFXVolume(_soudSaveSystem._soundData.sfxVolume / 100.0f);

        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<SliderValueChangeSignal>(OnSliderValueChanged);
        _eventBus.Subscribe<SliderEnableSignal>(OnSliderEnabled);
    }


    private void OnSliderValueChanged (SliderValueChangeSignal signal)
    {
        switch (signal.SliderType) {
            case SoundType.Master:
                _soundBusManager.SetMasterVolume(signal.Value);
                _soudSaveSystem._soundData.masterVolume = Mathf.Round(signal.Value * 100.0f);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
                _soudSaveSystem.SaveData();
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                break;
            case SoundType.Music:
                _soundBusManager.SetMusicVolume(signal.Value);
                _soudSaveSystem._soundData.musicVolume = Mathf.Round(signal.Value * 100.0f);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
                _soudSaveSystem.SaveData();
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                break;
            case SoundType.SFX:
                _soundBusManager.SetSFXVolume(signal.Value);
                _soudSaveSystem._soundData.sfxVolume = Mathf.Round(signal.Value * 100.0f);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!11
                _soudSaveSystem.SaveData();
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                break;
        }
    }


    private void OnSliderEnabled (SliderEnableSignal signal)
    {
        float value = GetSliderValue(signal.SliderType);
        _eventBus.Invoke(new SliderValueSetSignal(signal.SliderType, value));
    }


    private float GetSliderValue (SoundType sliderType)
    {
        switch (sliderType) {
            case SoundType.Master:
                return _soundBusManager.GetMasterVolume();
            case SoundType.Music:
                return _soundBusManager.GetMusicVolume();
            case SoundType.SFX:
                return _soundBusManager.GetSFXVolume();
            default:
                return 0;
        }
    }
}
