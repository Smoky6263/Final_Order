using UnityEngine;
using FMODUnity;

public class ParameterTrigger : MonoBehaviour
{
    FMOD.Studio.EventInstance music;

    private string _player = "Player";

    private void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Lvl_Music/Lvl_1");
        music.start();
    }

    public void setStartValue()
    {
        music.setParameterByName("Fight", 1f);
    }

    public void setExitValue()
    {
        music.setParameterByName("Fight", 0f);
    }
}
