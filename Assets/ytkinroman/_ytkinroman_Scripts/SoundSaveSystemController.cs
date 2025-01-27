using UnityEngine;

public class SoundSaveSystemController : MonoBehaviour
{
    [SerializeField] private SoundSaveSystem _soudSaveSystem;
    [SerializeField] private SoundBusManager _soundBusManager;

    [SerializeField] private SoundSlider _sliderMaster;
    [SerializeField] private SoundSlider _sliderMusic;
    [SerializeField] private SoundSlider _sliderSFX;

    public SoundBusManager SoundBusManager { get; private set; }

    public void Initialization () {
        if (_soudSaveSystem != null && _soundBusManager != null) {
            _soundBusManager.Initialization();
            _soudSaveSystem.Initialization();

            _soundBusManager.SetMasterVolume(_soudSaveSystem._soundData.masterVolume / 100.0f);
            _soundBusManager.SetMusicVolume(_soudSaveSystem._soundData.musicVolume / 100.0f);
            _soundBusManager.SetSFXVolume(_soudSaveSystem._soundData.sfxVolume / 100.0f);
        }
    }

    public void UpdateSoundData ()
    {
        bool allSlidersActive = (_sliderMaster != null && _sliderMaster.isActiveAndEnabled) &&
                                (_sliderMusic != null && _sliderMusic.isActiveAndEnabled) &&
                                (_sliderSFX != null && _sliderSFX.isActiveAndEnabled);

        if (allSlidersActive) {
            if (_sliderMaster != null) {
                _soudSaveSystem._soundData.masterVolume = _sliderMaster.GetSliderValue();
            }
            if (_sliderMusic != null) {
                _soudSaveSystem._soundData.musicVolume = _sliderMusic.GetSliderValue();
            }
            if (_sliderSFX != null) {
                _soudSaveSystem._soundData.sfxVolume = _sliderSFX.GetSliderValue();
            }

            _soudSaveSystem.SaveData();

            Debug.Log("стру€");
        }
    }
}
