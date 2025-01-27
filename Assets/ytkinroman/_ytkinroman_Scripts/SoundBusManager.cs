using FMOD.Studio;
using FMODUnity;


public class SoundBusManager
{
    private const string _busPathMaster = "bus:/";
    private const string _busPathMusic = "bus:/Music";
    private const string _busPathSFX = "bus:/SFX";

    private Bus _busMaster;
    private Bus _busMusic;
    private Bus _busSFX;


    public void Initialization ()
    {
        _busMaster = RuntimeManager.GetBus(_busPathMaster);
        _busMusic = RuntimeManager.GetBus(_busPathMusic);
        _busSFX = RuntimeManager.GetBus(_busPathSFX);
    }


    public float GetMasterVolume ()
    {
        _busMaster.getVolume(out float volume);
        return volume;
    }


    public void SetMasterVolume (float newValue)
    {
        float volume = newValue;
        _busMaster.setVolume(volume);
    }


    public float GetMusicVolume () 
    {
        _busMusic.getVolume(out float volume);
        return volume;
    }


    public void SetMusicVolume (float newValue) 
    {
        float volume = newValue;
        _busMusic.setVolume(volume);
    }


    public float GetSFXVolume ()
    {
        _busSFX.getVolume(out float volume);
        return volume;
    }


    public void SetSFXVolume (float newValue)
    {
        float volume = newValue;
        _busSFX.setVolume(volume);
    }
}
